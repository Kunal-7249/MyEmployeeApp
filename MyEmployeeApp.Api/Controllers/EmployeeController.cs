using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEmployeeApp.Domain.DTOs;
using MyEmployeeApp.Service.ServiceInterfaces;
using MyEmployeeApp.Service.Services;

namespace MyEmployeeApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adds a new employee to the system (PostgreSQL and Elasticsearch).
        /// </summary>
        /// <param name="dto">Employee data.</param>
        /// <returns>Success or error response.</returns>
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.AddEmployeeAsync(dto);
                return Ok("Employee added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Searches employees using Elasticsearch.
        /// </summary>
        /// <param name="dto">Search filters like name, age, salary.</param>
        /// <returns>List of matching employees.</returns>
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] EmployeeSearchDto dto)
        {
            try
            {
                var results = await _service.SearchEmployeesAsync(dto);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Search failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Partially updates an existing employee.
        /// Only non-null fields in the request body will be updated.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <param name="dto">Fields to update.</param>
        /// <returns>Success or not found response.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] EmployeeUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await _service.PartialUpdateAsync(id, dto);
                if (!success)
                    return NotFound("Employee not found.");

                return Ok("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Update error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an employee from PostgreSQL and Elasticsearch.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <returns>Success or not found response.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _service.DeleteEmployeeAsync(id);
                if (!deleted)
                    return NotFound("Employee not found.");

                return Ok("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Delete error: {ex.Message}");
            }
        }

    }
}
