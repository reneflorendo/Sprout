using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity updatedEntity, TEntity existingEntity);
        Task DeleteAsync(int id);
    }
}
