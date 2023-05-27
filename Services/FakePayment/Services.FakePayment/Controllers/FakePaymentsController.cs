using Microsoft.AspNetCore.Mvc; 
using Shared.ControllerBases;
using Shared.Dtos; 

namespace Services.FakePayment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FakePaymentsController : CustomBaseController
{
    [HttpPost]
    public IActionResult ReceivePayment()
    {
        return CreateActionResultInstance(Response<NoContent>.Success(200));
    }
}