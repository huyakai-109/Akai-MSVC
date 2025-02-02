using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository : RepositoryBaseAsync<Entities.Product, long, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public Task Create(Entities.Product product) => CreateAsync(product);

        public async Task Delete(long id)
        {
           var product = await GetById(id);
           if(product != null)  await DeleteAsync(product);
        }

        public async Task<IEnumerable<Entities.Product>> GetAll() => await FindAll().ToArrayAsync();

        public async Task<Entities.Product?> GetById(long id) => await GetByIdAsync(id);

        public Task<Entities.Product?> GetProductByNo(string no) => FindByCondition(x => x.No == no).SingleOrDefaultAsync();

        public Task Update(Entities.Product product) => UpdateAsync(product);
    }
}
