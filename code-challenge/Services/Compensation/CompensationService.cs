using System;
using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<EmployeeService> _logger;

        public CompensationService(ILogger<EmployeeService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }
        
        public Compensation CreateCompensation(Compensation compensation)
        {
            
            if (compensation != null)
            {
                _compensationRepository.CreateCompensation(compensation);
                _compensationRepository.SaveAsync().Wait();
            }
            
            return compensation;
        }

        public Compensation ReadCompensation(String id)
        {
            return !String.IsNullOrEmpty(id) ? _compensationRepository.ReadCompensation(id) : null;
        }
    }
}