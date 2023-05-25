using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Shared.ControllerBases;

public class CustomBaseController : ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(Response<T> response) 
        => new ObjectResult(response) { StatusCode = response.StatusCode }; 
}