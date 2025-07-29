using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Domain.DTOs
{
    public class EmployeeUpdateDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Salary { get; set; }

        public DateTime? BirthDate { get; set; }
    }

}
