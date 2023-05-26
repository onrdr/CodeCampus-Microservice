using AutoMapper;
using Services.Order.Domain.Dtos;
using Services.Order.Domain.OrderAggregate;

namespace Services.Order.Application.Mapping;

internal class CustomMapping : Profile
{
    public CustomMapping()
    {
        CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}