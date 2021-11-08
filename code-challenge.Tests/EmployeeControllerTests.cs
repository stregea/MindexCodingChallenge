using challenge.Controllers;
using challenge.Data;
using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using code_challenge.Tests.Integration.Helpers;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class EmployeeControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateEmployee_Returns_Created()
        {
            // Arrange
            var employee = new Employee()
            {
                Department = "Complaints",
                FirstName = "Debbie",
                LastName = "Downer",
                Position = "Receiver",
            };

            var requestContent = new JsonSerialization().ToJson(employee);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/employee",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newEmployee = response.DeserializeContent<Employee>();
            Assert.IsNotNull(newEmployee.EmployeeId);
            Assert.AreEqual(employee.FirstName, newEmployee.FirstName);
            Assert.AreEqual(employee.LastName, newEmployee.LastName);
            Assert.AreEqual(employee.Department, newEmployee.Department);
            Assert.AreEqual(employee.Position, newEmployee.Position);
        }

        [TestMethod]
        public void GetEmployeeById_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";
            
            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var employee = response.DeserializeContent<Employee>();
            Assert.AreEqual(expectedFirstName, employee.FirstName);
            Assert.AreEqual(expectedLastName, employee.LastName);
        }

        [TestMethod]
        public void UpdateEmployee_Returns_Ok()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                Department = "Engineering",
                FirstName = "Pete",
                LastName = "Best",
                Position = "Developer VI",
            };
            var requestContent = new JsonSerialization().ToJson(employee);

            // Execute
            var putRequestTask = _httpClient.PutAsync($"api/employee/{employee.EmployeeId}",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var putResponse = putRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode);
            var newEmployee = putResponse.DeserializeContent<Employee>();

            Assert.AreEqual(employee.FirstName, newEmployee.FirstName);
            Assert.AreEqual(employee.LastName, newEmployee.LastName);
        }

        [TestMethod]
        public void UpdateEmployee_Returns_NotFound()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "Invalid_Id",
                Department = "Music",
                FirstName = "Sunny",
                LastName = "Bono",
                Position = "Singer/Song Writer",
            };
            var requestContent = new JsonSerialization().ToJson(employee);

            // Execute
            var postRequestTask = _httpClient.PutAsync($"api/employee/{employee.EmployeeId}",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void CreateReportingStructure_Returns_Ok()
        {
            // Arrange
            var expectedResponseValue = 2;
            var dev1 = new Employee()
            {
                Department = "Engineering",
                FirstName = "Sam",
                LastName = "Tregea",
                Position = "Junior Developer I",
            };
            var dev2 = new Employee()
            {
                Department = "Engineering",
                FirstName = "Abraham",
                LastName = "Lincoln",
                Position = "Developer VI",
            };
            var teamLead = new Employee()
            {
                Department = "Engineering",
                FirstName = "George",
                LastName = "Washington",
                Position = "Team Leader",
                DirectReports = new List<Employee>() {dev1, dev2}
            };

            // Execute

            // insert dev1 into the database.
            var insertDev1Request = new JsonSerialization().ToJson(dev1);
            var postRequestTask1 = _httpClient.PostAsync("api/employee",
                new StringContent(insertDev1Request, Encoding.UTF8, "application/json"));
            var response1 = postRequestTask1.Result;

            // Assert successful insertion.
            Assert.AreEqual(HttpStatusCode.Created, response1.StatusCode);

            // insert dev2 into the database.
            var insertDev2Request = new JsonSerialization().ToJson(dev2);
            var postRequestTask2 = _httpClient.PostAsync("api/employee",
                new StringContent(insertDev2Request, Encoding.UTF8, "application/json"));
            var response2 = postRequestTask2.Result;

            // Assert successful insertion.
            Assert.AreEqual(HttpStatusCode.Created, response2.StatusCode);

            // insert team_lead into the database.
            var insertTeamLeadRequest = new JsonSerialization().ToJson(teamLead);
            var postRequestTask3 = _httpClient.PostAsync("api/employee",
                new StringContent(insertTeamLeadRequest, Encoding.UTF8, "application/json"));
            var response3 = postRequestTask3.Result;

            // Assert successful insertion.
            Assert.AreEqual(HttpStatusCode.Created, response3.StatusCode);

            // Read the team leader employee response from the server, and Assert not null.
            var employee = response3.DeserializeContent<Employee>();
            Assert.IsNotNull(employee);
            Assert.IsNotNull(employee.DirectReports);
            
            // Calculate the reporting structure of the employee.
            var getReportingStructureTask = _httpClient.GetAsync($"api/employee/reportingStructure/{employee.EmployeeId}");
            var response4 = getReportingStructureTask.Result;
            var reportingStructure = response4.DeserializeContent<ReportingStructure>();

            // Assert the expected and actual values.
            Assert.AreEqual(HttpStatusCode.OK, response4.StatusCode);
            Assert.AreEqual(expectedResponseValue, reportingStructure.numberOfReports);
        }
        
    }
}