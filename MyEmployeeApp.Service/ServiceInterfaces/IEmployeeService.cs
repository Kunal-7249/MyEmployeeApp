using MyEmployeeApp.Domain.DTOs;
using MyEmployeeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Service.ServiceInterfaces
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(EmployeeDto dto);
        Task<IEnumerable<Employee>> SearchEmployeesAsync(string query);
        //Task<bool> UpdateEmployeeAsync(Guid id, EmployeeDto dto);
        Task<bool> DeleteEmployeeAsync(Guid id);
        Task<bool> PartialUpdateAsync(Guid id, EmployeeUpdateDto dto);

    }
}
