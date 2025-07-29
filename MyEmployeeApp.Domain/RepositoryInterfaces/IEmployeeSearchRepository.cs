using MyEmployeeApp.Domain.DTOs;
using MyEmployeeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Domain.RepositoryInterfaces
{

    public interface IEmployeeSearchRepository
    {
        Task<IEnumerable<Employee>> SearchAsync(EmployeeSearchDto dto);
    }
}
