using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories;

public class CustomerRepository(CustomerContext context, IUnitOfWork<CustomerContext> unitOfWork) :RepositoryBaseAsync<Entities.Customer,Guid,CustomerContext>(context, unitOfWork),ICustomerRepository
{
    public Task<Entities.Customer?> GetByUserNameAsync(string userName) => FindByCondition(x=>x.UserName==userName).SingleOrDefaultAsync();
    public async Task<IEnumerable<Entities.Customer>> GetCustomersAsync() => await FindAll().ToListAsync();

}