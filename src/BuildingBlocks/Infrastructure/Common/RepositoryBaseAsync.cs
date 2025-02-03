using System.Linq.Expressions;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common
{
    public class RepositoryBaseAsync<T, K, TContext>(
        TContext dbContext,
        IUnitOfWork<TContext> unitOfWork) : IRepositoryBaseAsync<T, K, TContext> where T : EntityBase<K> where TContext : DbContext
    {
        public Task<IDbContextTransaction> BeginTransactionAsync() => dbContext.Database.BeginTransactionAsync();

        public async Task EndTransactionAsync()
        {
            await SaveChangeAsync();
            await dbContext.Database.CommitTransactionAsync();
        }
        public Task RollbackTransactionAsync() => dbContext.Database.RollbackTransactionAsync();

        public Task<int> SaveChangeAsync() => unitOfWork.CommitAsync();

        public async Task<K> CreateAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            return entity.Id;
        }

        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
            return entities.Select(e => e.Id).ToList();
        }

        public Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            if (dbContext.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;

            T exist = dbContext.Set<T>().Find(entity.Id)!;
            dbContext.Entry(exist).CurrentValues.SetValues(entity);

            return Task.CompletedTask;
        }

        public Task UpdateListAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteListAsync(IEnumerable<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges ? dbContext.Set<T>().AsNoTracking()
                          : dbContext.Set<T>(); 

        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));

            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges ? dbContext.Set<T>().Where(expression).AsNoTracking()
                          : dbContext.Set<T>().Where(expression);       

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));

            return items;
        }

        public async Task<T?> GetByIdAsync(K id) => await dbContext.Set<T>().FindAsync(id);

        public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
            await FindByCondition(x => x.Equals(id), trackChanges: false, includeProperties).FirstOrDefaultAsync();
    }
}
