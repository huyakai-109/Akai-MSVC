using Contracts.Common.Interfaces;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBaseAsync<Entities.Product, long, ProductContext>
    {
        Task<IEnumerable<Entities.Product>> GetAll();

        Task<Entities.Product?> GetById(long id);

        Task<Entities.Product?> GetProductByNo(string no);

        Task Create(Entities.Product product);

        Task Update(Entities.Product product);

        Task Delete(long id);
    }
}
