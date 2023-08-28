using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Sprout.Exam.DataAccess.Data;
using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.DataAccess.Repositories.Contracts;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DatabaseContext dbContext) : base(dbContext)
        {

        }
        public async Task<int> AddEmployeeAsync(Employee entity)
        {
            var employee = await base.AddAsync(entity);

            return employee.Id;
        }

        public async Task<IEnumerable<Employee>> GetAllActiveEmployeeAsync()
        {
            var employees =  await base.GetAllAsync();

            return employees.Where( employee=>!employee.IsDeleted);
        }
    }
}
