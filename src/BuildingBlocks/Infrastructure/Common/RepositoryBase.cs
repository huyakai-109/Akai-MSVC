using System.Linq.Expressions;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common
{
    public class RepositoryBase<T, K, TContext> : RepositoryQueryBase<T, K, TContext>, 
                                            IRepositoryBaseAsync<T, K, TContext> 
                                            where T : EntityBase<K> 
                                            where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        public RepositoryBase(TContext dbcontext, IUnitOfWork<TContext> unitOfWork) : base(dbcontext)
        {
            _dbContext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task<IDbContextTransaction> BeginTransactionAsync() => _dbContext.Database.BeginTransactionAsync();

        public async Task EndTransactionAsync()
        {
            await SaveChangeAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }
        public Task RollbackTransactionAsync() => _dbContext.Database.RollbackTransactionAsync();

        public Task<int> SaveChangeAsync() => _unitOfWork.CommitAsync();

        public async Task<K> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity.Id;
        }

        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return entities.Select(e => e.Id).ToList();
        }

        public Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            if (_dbContext.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;

            T exist = _dbContext.Set<T>().Find(entity.Id)!;
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);

            return Task.CompletedTask;
        }

        public Task UpdateListAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
