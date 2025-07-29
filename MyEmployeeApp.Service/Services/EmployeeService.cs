using MyEmployeeApp.Domain.DTOs;
using MyEmployeeApp.Domain.Entities;
using MyEmployeeApp.Domain.RepositoryInterfaces;
using MyEmployeeApp.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeSearchRepository _employeeSearchRepository;

        public EmployeeService(IEmployeeRepository repository, IEmployeeSearchRepository employeeSearchRepository)
        {
            _repository = repository;
            _employeeSearchRepository = employeeSearchRepository;
        }

        public async Task AddEmployeeAsync(EmployeeDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Address = dto.Address,
                Salary = dto.Salary,
                BirthDate = dto.BirthDate
            };

            await _repository.AddAsync(employee);
        }

        public async Task<IEnumerable<Employee>> SearchEmployeesAsync(EmployeeSearchDto dto)
        {
            return await _employeeSearchRepository.SearchAsync(dto);
        }


        public async Task<bool> PartialUpdateAsync(Guid id, EmployeeUpdateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return false;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                employee.Name = dto.Name;

            if (dto.Salary.HasValue)
                employee.Salary = dto.Salary.Value;

            if (!string.IsNullOrWhiteSpace(dto.Address))
                employee.Address = dto.Address;

            if (dto.BirthDate.HasValue)
                employee.BirthDate = dto.BirthDate.Value;

            await _repository.UpdateAsync(employee);
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return false;

            await _repository.DeleteAsync(employee);
            return true;
        }

    }
}
