using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using System;
using System.Threading.Tasks;

using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// Fetch employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _employeeService.GetEmployeesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // Return a 500 Internal Server Error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Get specific employee by employeeId.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // Return a 500 Internal Server Error response
                return StatusCode(500, "An error occurred while processing the request.");

            }
            
        }

        /// <summary>
        /// Update specific employee
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(input);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // Return a 500 Internal Server Error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
            
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            try
            {
                var id = await _employeeService.AddEmployeeAsync(input);

                return Created($"/api/employees/{id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // Return a 500 Internal Server Error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
            
        }

        /// <summary>
        /// Delete specific employee
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.SoftDeleteEmployeeAsync(id);

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // Return a 500 Internal Server Error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
            
        }

        /// <summary>
        /// Calculate employee salary
        /// </summary>
        /// <param name="id"></param>
        /// <param name="noOfAbsenceorWorkDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate/{noOfAbsenceorWorkDays}")]
        public async Task<IActionResult> Calculate(int id, decimal noOfAbsenceorWorkDays)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    return NotFound();
                }

                var type = (EmployeeType)employee.TypeId;

                if (!Enum.IsDefined(typeof(EmployeeType), type))
                {
                    NotFound("Employee Type not found");
                }

                var result = await _employeeService.CalculateSalaryAsync(type, noOfAbsenceorWorkDays);

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // Return a 500 Internal Server Error response
                return StatusCode(500, "An error occurred while processing the request.");
            }            
            
        }

    }
}
