using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController: ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> TestAPI()
    {
        return Ok("Testing API");
    }
}