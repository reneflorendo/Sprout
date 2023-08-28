using Sprout.Exam.DataAccess.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        public Task<int> AddEmployeeAsync(Employee entity);

        public Task<IEnumerable<Employee>> GetAllActiveEmployeeAsync();


    }
}
