using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Domain.DTOs
{
    public class EmployeeSearchDto
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? AgeComparison { get; set; }

        public decimal? Salary { get; set; }
        public string? SalaryComparison { get; set; } 
    }

}
