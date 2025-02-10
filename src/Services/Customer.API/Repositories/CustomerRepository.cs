using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class CustomerRepository : RepositoryBase<Entities.Customer, int, CustomerContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task Create(Entities.Customer customer) => await CreateAsync(customer);

        public async Task Delete(int id)
        {
            var customer = await GetById(id);
            if (customer != null) await DeleteAsync(customer);
        }

        public async Task<IEnumerable<Entities.Customer>> GetAll() => await FindAll().ToArrayAsync();

        public async Task<Entities.Customer?> GetById(int id) => await GetByIdAsync(id);

        public async Task<Entities.Customer?> GetCustomerByUserName(string userName) =>
           await FindByCondition( x => x.UserName.Equals(userName)).SingleOrDefaultAsync();

        public async Task Update(Entities.Customer customer) => await UpdateAsync(customer);
    }
}
