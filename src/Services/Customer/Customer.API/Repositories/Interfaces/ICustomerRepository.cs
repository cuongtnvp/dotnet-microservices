using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces;

public interface ICustomerRepository:IRepositoryQueryBase<Entities.Customer,Guid,CustomerContext>
{
    Task<Entities.Customer?> GetByUserNameAsync(string userName);
    Task<IEnumerable<Entities.Customer>> GetCustomersAsync();
}