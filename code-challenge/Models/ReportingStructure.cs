using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Models
{
    public class ReportingStructure
    {
        // The selected employee for the Reporting Structure.
        public Employee employee { get; set; }

        // The number of reports associated with the number of directReports for an employee
        // and all of their direct reports.
        public int numberOfReports { get; set; }
    }
}