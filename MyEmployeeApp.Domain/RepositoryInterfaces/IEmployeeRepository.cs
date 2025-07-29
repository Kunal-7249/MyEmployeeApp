using MyEmployeeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Domain.RepositoryInterfaces
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(Guid id);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);

    }
}
