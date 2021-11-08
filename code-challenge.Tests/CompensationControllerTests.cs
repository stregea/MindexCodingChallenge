using System;
using System.Net;
using System.Net.Http;
using System.Text;
using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using code_challenge.Tests.Integration.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
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
        public void CreateCompensation_Returns_Ok()
        {
            /*
             * Arrange
             */
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";
            var expectedSalary = 100000;
            var expectedDate = DateTime.MaxValue;

            /*
             * Execute
             */

            var getEmployeeRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}");
            var employeeResponse = getEmployeeRequestTask.Result;

            // Assert an OK employee retrieval response, then assert if the expected employee.
            Assert.AreEqual(HttpStatusCode.OK, employeeResponse.StatusCode);
            var employee = employeeResponse.DeserializeContent<Employee>();
            Assert.AreEqual(expectedFirstName, employee.FirstName);
            Assert.AreEqual(expectedLastName, employee.LastName);

            // Create a compensation and insert into the database.
            var compensationRequestContent = new JsonSerialization().ToJson(
                new Compensation()
                {
                    employee = employee,
                    salary = 100000,
                    effectiveDate = DateTime.MaxValue
                }
            );
            
            // Test posting a compensation to the server.
            var postCompensationRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(compensationRequestContent, Encoding.UTF8, "application/json"));
            var response = postCompensationRequestTask.Result;
            
            // Assert successful insertion.
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            // Assert if the same employee as above.
            var compensationResponseContent = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(compensationResponseContent);
            Assert.AreEqual(employeeId, compensationResponseContent.employee.EmployeeId);
            Assert.AreEqual(expectedFirstName, compensationResponseContent.employee.FirstName);
            Assert.AreEqual(expectedLastName, compensationResponseContent.employee.LastName);
            Assert.AreEqual(expectedSalary, compensationResponseContent.salary);
            Assert.AreEqual(expectedDate, compensationResponseContent.effectiveDate);
        }
    }
}