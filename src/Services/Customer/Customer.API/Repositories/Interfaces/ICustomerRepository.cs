using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces;

public interface ICustomerRepository:IRepositoryBaseAsync<Entities.Customer,Guid,CustomerContext>
{
    Task<Entities.Customer?> GetByUserNameAsync(string userName);
    Task<IEnumerable<Entities.Customer>> GetCustomersAsync();
    Task<Entities.Customer?> GetByEmailAsync(string emailAddress);
    Task AddCustomerAsync(Entities.Customer customer);
    Task UpdateCustomerAsync(Entities.Customer customer);
    
    Task<Entities.Customer?> GetCustomerByIdAsync(Guid customerId);
    
    Task DeleteCustomerAsync(Entities.Customer customer);

}