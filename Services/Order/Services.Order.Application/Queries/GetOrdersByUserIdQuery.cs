using MediatR;
using Services.Order.Domain.Dtos;
using Shared.Dtos;

namespace Services.Order.Application.Queries;

public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
{
    public string UserId { get; set; }
}