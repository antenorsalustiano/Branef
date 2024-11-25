using BRANEF.Domain.Entities.Base;
using System.Linq.Expressions;

namespace BRANEF.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : EntidadeBase
    {
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);
        Task<TEntity?> GetById(int id, CancellationToken cancellationToken = default);
        Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> Delete(TEntity entity, CancellationToken cancellationToken = default);
        Task<List<TEntity>> FilterBy(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default);
        Task AddMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);        
    }
}
