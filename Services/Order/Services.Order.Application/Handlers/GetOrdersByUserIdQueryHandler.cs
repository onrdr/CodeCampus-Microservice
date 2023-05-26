using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Order.Domain.Dtos;
using Services.Order.Application.Mapping;
using Services.Order.Infrastructure;
using Shared.Dtos;
using Services.Order.Application.Queries;

namespace Services.Order.Application.Handlers;

internal class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
{
    private readonly OrderDbContext _context;

    public GetOrdersByUserIdQueryHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(x => x.OrderItems)
            .Where(x => x.BuyerId == request.UserId)
            .ToListAsync(cancellationToken: cancellationToken);

        if (!orders.Any())
        {
            return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
        }

        var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

        return Response<List<OrderDto>>.Success(ordersDto, 200);
    }
}