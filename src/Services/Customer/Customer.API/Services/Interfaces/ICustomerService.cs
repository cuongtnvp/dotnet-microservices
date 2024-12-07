using Shared.Dtos.Customers;

namespace Customer.API.Services.Interfaces;

public interface ICustomerService
{
    Task<CustomerDto> GetByUserNameAsync(string userName);
    
    Task<CustomerDto> GetCustomerByIdAsync(Guid id);


    Task<List<CustomerDto>> GetCustomersAsync();
    Task<CustomerDto?> UpdateCustomerAsync(Guid id, UpdateCustomerDto customerDto);

    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto customerDto);
    Task DeleteCustomerAsync(Guid id);
    
    
}