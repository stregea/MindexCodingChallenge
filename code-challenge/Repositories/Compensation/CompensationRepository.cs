using System;
using System.Linq;
using System.Threading.Tasks;
using challenge.Data;
using challenge.Models;
using Microsoft.Extensions.Logging;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }
        
        public Compensation CreateCompensation(Compensation compensation)
        {
            // Add id to serve as primary key in the database.
            compensation.compensationId = Guid.NewGuid().ToString();
            _compensationContext.EmployeeCompensations.Add(compensation);
            return compensation;
        }

        public Compensation ReadCompensation(string id)
        {
            return _compensationContext.EmployeeCompensations.SingleOrDefault(c => c.employee.EmployeeId == id);
        }
        
        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}