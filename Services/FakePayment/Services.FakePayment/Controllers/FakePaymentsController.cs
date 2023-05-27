using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Services.FakePayment.Models;
using Shared.ControllerBases;
using Shared.Dtos;
using Shared.Messages;

namespace Services.FakePayment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FakePaymentsController : CustomBaseController
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost]
    public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
    {
        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

        var createOrderMessageCommand = new CreateOrderMessageCommand
        {
            BuyerId = paymentDto.Order.BuyerId,
            Province = paymentDto.Order.Address.Province,
            District = paymentDto.Order.Address.District,
            Street = paymentDto.Order.Address.Street,
            Line = paymentDto.Order.Address.Line,
            ZipCode = paymentDto.Order.Address.ZipCode
        };

        paymentDto.Order.OrderItems.ForEach(x =>
        {
            createOrderMessageCommand.OrderItems.Add(new OrderItem
            {
                PictureUrl = x.PictureUrl,
                Price = x.Price,
                ProductId = x.ProductId,
                ProductName = x.ProductName
            });
        });

        await sendEndpoint.Send(createOrderMessageCommand);

        return CreateActionResultInstance(Shared.Dtos.Response<NoContent>.Success(200));
    }
}