using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBaseAsync<Entities.Customer, int, CustomerContext>
    {
        Task<IEnumerable<Entities.Customer>> GetAll();

        Task<Entities.Customer?> GetById(int id);

        Task<Entities.Customer?> GetCustomerByUserName(string userName);

        Task Create(Entities.Customer customer);

        Task Update(Entities.Customer customer);

        Task Delete(int id);
    }
}
