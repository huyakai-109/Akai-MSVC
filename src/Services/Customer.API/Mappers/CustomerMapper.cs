using AutoMapper;
using Shared.DTOs;

namespace Customer.API.Mappers
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<Entities.Customer, CustomerDto>();
            CreateMap<CreateUpdateCustomerDto, Entities.Customer>();
        }
    }
}
