using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.Customers;

namespace Customer.API.Repositories;

public class CustomerRepository(CustomerContext context, IUnitOfWork<CustomerContext> unitOfWork)
    : RepositoryBaseAsync<Entities.Customer, Guid, CustomerContext>(context, unitOfWork), ICustomerRepository
{
    public Task<Entities.Customer?> GetByUserNameAsync(string userName) =>
        FindByCondition(x => x.UserName == userName).SingleOrDefaultAsync();

    public async Task<IEnumerable<Entities.Customer>> GetCustomersAsync() => await FindAll().ToListAsync();

    public async Task<Entities.Customer?> GetByEmailAsync(string emailAddress)
        => await FindByCondition(x => x.EmailAddress == emailAddress).SingleOrDefaultAsync();


    public Task AddCustomerAsync(Entities.Customer customer)
    {
        return CreateAsync(customer);
    }

    public Task UpdateCustomerAsync(Entities.Customer customer)
    {
        return UpdateAsync(customer);
    }

    public Task<Entities.Customer?> GetCustomerByIdAsync(Guid customerId)
    {
        return FindByCondition(x => x.Id == customerId).SingleOrDefaultAsync();
    }

    public Task DeleteCustomerAsync(Entities.Customer customer)
    {
       return DeleteAsync(customer);
    }
}