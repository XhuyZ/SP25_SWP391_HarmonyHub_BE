using Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route(UriConstants.AUTH_BASE_URI)]
[ApiController]
public class AuthController: ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> TestAPI()
    {
        return Ok("Testing API");
    }
}