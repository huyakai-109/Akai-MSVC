using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository : RepositoryBase<Entities.Product, long, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task Create(Entities.Product product) => await CreateAsync(product);

        public async Task Delete(long id)
        {
           var product = await GetById(id);
           if(product != null)  await DeleteAsync(product);
        }

        public async Task<IEnumerable<Entities.Product>> GetAll() => await FindAll().ToArrayAsync();

        public async Task<Entities.Product?> GetById(long id) => await GetByIdAsync(id);

        public async Task<Entities.Product?> GetProductByNo(string no) => await FindByCondition(x => x.No == no).SingleOrDefaultAsync();

        public async Task Update(Entities.Product product) => await UpdateAsync(product);
    }
}
