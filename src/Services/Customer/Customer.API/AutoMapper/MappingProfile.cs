using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Dtos.Customers;

namespace Customer.API.AutoMapper;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Entities.Customer,CustomerDto>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
    }

    
}