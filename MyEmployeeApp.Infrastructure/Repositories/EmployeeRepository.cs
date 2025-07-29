using Microsoft.EntityFrameworkCore;
using MyEmployeeApp.Domain.Entities;
using MyEmployeeApp.Domain.RepositoryInterfaces;
using MyEmployeeApp.Infrastructure.Data;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly IElasticClient _elasticClient;

        public EmployeeRepository(AppDbContext context, IElasticClient elasticClient)
        {
            _context = context;
            _elasticClient = elasticClient;
        }

        public async Task AddAsync(Employee employee)
        {

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            await _elasticClient.IndexDocumentAsync(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            await _elasticClient.IndexDocumentAsync(employee);
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            await _elasticClient.DeleteAsync<Employee>(employee.Id);
        }
    }
}
