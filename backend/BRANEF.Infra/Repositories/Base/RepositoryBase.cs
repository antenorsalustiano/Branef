using BRANEF.Domain.Entities.Base;
using BRANEF.Domain.Interfaces;
using BRANEF.Infra.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BRANEF.Infra.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntidadeBase
    {
        protected readonly BranefContext _context;

        public RepositoryBase(BranefContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task AddMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().AddRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<List<TEntity>> FilterBy(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default) 
            => await GetSet().Where(filterExpression).ToListAsync(cancellationToken);

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default) 
            => await GetSet().ToListAsync(cancellationToken);

        public async Task<TEntity?> GetById(int id, CancellationToken cancellationToken = default) 
            => await GetSet().GetById(id);

        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }    

        private DbSet<TEntity> GetSet()
        {
            return _context.Set<TEntity>();
        }
    }
}

public static class QueryableExtensions
{
    public static Task<T?> GetById<T>(this IQueryable<T> query, int id)
    {
        // e => e.Id == id
        var parameter = Expression.Parameter(typeof(T));
        var left = Expression.Property(parameter, "Id");
        var right = Expression.Constant(id);
        var equal = Expression.Equal(left, right);
        var byId = Expression.Lambda<Func<T, bool>>(equal, parameter);

        return query.SingleOrDefaultAsync(byId);
    }
}
