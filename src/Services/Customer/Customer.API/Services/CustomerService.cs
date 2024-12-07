using AutoMapper;
using Customer.API.Exceptions;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;
using Shared.Dtos.Customers;

namespace Customer.API.Services;

public class CustomerService(ICustomerRepository repository, IMapper mapper) : ICustomerService
{
    public async Task<CustomerDto> GetByUserNameAsync(string userName)
    {
        var customer = await repository.GetByUserNameAsync(userName);

        var customerDto = mapper.Map<CustomerDto>(customer);

        return customerDto;
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(Guid id)
    {
        var customerEntity = await repository.GetCustomerByIdAsync(id);
        if (customerEntity == null)
        {
            throw new NotFoundException(id.ToString());
        }
        var customerDto = mapper.Map<CustomerDto>(customerEntity);
        return customerDto;
    }

    public async Task<List<CustomerDto>> GetCustomersAsync()
    {
        var customers = await repository.GetCustomersAsync();
        if (customers == null)
        {
            throw new NotFoundException("Dont have any customers");
        }
        var customerDto = mapper.Map<List<CustomerDto>>(customers);
        return customerDto;
    }

    public async Task<CustomerDto?> UpdateCustomerAsync(Guid id, UpdateCustomerDto updateCustomerDto)
    {
        var customerEntity = await repository.GetByIdAsync(id);
        if (customerEntity == null)
        {
            throw new NotFoundException(id.ToString());
        }

        var updateCustomer = mapper.Map(updateCustomerDto, customerEntity);
        await repository.UpdateCustomerAsync(updateCustomer);
        await repository.SaveChangesAsync();
        var customerDto = mapper.Map<CustomerDto>(updateCustomer);
        return customerDto;
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        // Kiểm tra sự tồn tại của UserName
        var existingCustomerByUserName = await repository.GetByUserNameAsync(createCustomerDto.UserName);
        if (existingCustomerByUserName != null)
        {
            throw new ExistingFieldException(createCustomerDto.UserName, nameof(createCustomerDto.UserName));
        }

        // Kiểm tra sự tồn tại của Email
        var existingCustomerByEmail = await repository.GetByEmailAsync(createCustomerDto.EmailAddress);
        if (existingCustomerByEmail != null)
        {
            throw new ExistingFieldException(createCustomerDto.EmailAddress, nameof(createCustomerDto.EmailAddress));
        }

        // Ánh xạ DTO sang thực thể Customer
        var customer = mapper.Map<Entities.Customer>(createCustomerDto);

        // Lưu Customer vào cơ sở dữ liệu
        await repository.AddCustomerAsync(customer);
        await repository.SaveChangesAsync();

        // Ánh xạ thực thể Customer sang DTO để trả về
        var customerDto = mapper.Map<CustomerDto>(customer);

        return customerDto;
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        var customerEntity = await repository.GetCustomerByIdAsync(id);
        if(customerEntity == null) throw new NotFoundException(id.ToString());
        await repository.DeleteCustomerAsync(customerEntity);
        await repository.SaveChangesAsync();

    }
}