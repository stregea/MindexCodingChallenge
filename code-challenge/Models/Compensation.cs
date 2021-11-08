using System;

namespace challenge.Models
{
    public class Compensation
    {
        // The id of the compensation.
        public string compensationId { get; set; }
        
        // The employee to associate the compensation with.
        public Employee employee { get; set; }
        
        // The salary for the employee.
        public long salary { get; set; }
        
        // The effective date in which the employee will receive the salary.
        public DateTime effectiveDate { get; set; }
    }
}