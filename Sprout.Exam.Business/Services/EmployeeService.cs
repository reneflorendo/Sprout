using AutoMapper;

using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Factories.Contracts;
using Sprout.Exam.Business.Services.Contracts;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.DataAccess.Repositories.Contracts;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Services
{
    /// <summary>
    /// Business logic and CRUD for Employee object
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly ISalaryCalculationFactory _salaryCalculationFactory;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, ISalaryCalculationFactory salaryCalculationFactory, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _salaryCalculationFactory = salaryCalculationFactory;
            _mapper = mapper;
        }

        public Task<int> AddEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                return _employeeRepository.AddEmployeeAsync(employee);

            }            
            catch (Exception) { 
                throw; 
            }
        }

        public async Task<decimal> CalculateSalaryAsync(EmployeeType employeeType, decimal noOfAbsenceorWorkDays)
        {
            try
            {
                if (noOfAbsenceorWorkDays < 0)
                {
                    throw new ArgumentException("Number of absence/work days cannot be negative.");
                }

                var strategy = _salaryCalculationFactory.GetSalaryCalculation(employeeType);
                var salary = await strategy.CalculateSalary(noOfAbsenceorWorkDays);
                return salary;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                await _employeeRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                var mapEmployee = _mapper.Map<EmployeeDto>(employee);

                return mapEmployee;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllActiveEmployeeAsync();
                var mapEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

                return mapEmployees;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task SoftDeleteEmployeeAsync(int id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);

            if (existingEmployee != null)
            {
                existingEmployee.IsDeleted = true;
                var employee = _mapper.Map<Employee>(existingEmployee);
                await _employeeRepository.UpdateAsync(employee, existingEmployee);
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(EditEmployeeDto editEmployeeDto)
        {
            try
            {
                var existingEmployee = await _employeeRepository.GetByIdAsync(editEmployeeDto.Id);

                if (existingEmployee != null)
                {
                    var employee = _mapper.Map<Employee>(editEmployeeDto);
                    return await _employeeRepository.UpdateAsync(employee, existingEmployee);
                }

                return null;                

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
