using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;
using challenge.Repositories;
namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        
        // private readonly 
        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.employee.FirstName} {compensation.employee.LastName}.'");

            _compensationService.CreateCompensation(compensation);
            
            return CreatedAtRoute("getCompensationById", new {id = compensation.employee.EmployeeId}, compensation);
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult ReadCompensation(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");
            
            Compensation compensation = _compensationService.ReadCompensation(id);

            if (compensation == null)
            {
                return NotFound();
            }
            
            return Ok(compensation);
        }
    }
}