using AutoMapper;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;
using Shared.Dtos.Customers;

namespace Customer.API.Services;

public class CustomerService(ICustomerRepository repository,IMapper mapper) : ICustomerService
{
    public async Task<IResult> GetByUserNameAsync(string userName)
    {
        var customer = await repository.GetByUserNameAsync(userName);
        if (customer == null) return Results.NoContent();
        var customerDto = mapper.Map<CustomerDto>(customer);

        return Results.Ok(customerDto);
    } 

    public async Task<IResult> GetCustomersAsync()
        => Results.Ok(await repository.GetCustomersAsync());
}