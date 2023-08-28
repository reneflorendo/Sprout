using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<int> AddEmployeeAsync(CreateEmployeeDto employee);
        Task<Employee> UpdateEmployeeAsync(EditEmployeeDto employee);
        Task DeleteEmployeeAsync(int id);
        Task SoftDeleteEmployeeAsync(int id);
        Task<decimal> CalculateSalaryAsync(EmployeeType id, decimal noOfAbsenceorWorkDays);
    }
}
