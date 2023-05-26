using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Order.Application.Commands;
using Services.Order.Application.Queries;
using Shared.ControllerBases;
using Shared.Services;

namespace Services.Order.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : CustomBaseController
{
    private readonly IMediator _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
    {
        _mediator = mediator;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var response = await _mediator
            .Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });

        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
    {
        var response = await _mediator.Send(createOrderCommand);

        return CreateActionResultInstance(response);
    }
}