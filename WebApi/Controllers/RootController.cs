using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("")]
[ApiController]
public class RootController : BaseApiController
{

    [HttpGet("")]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Root()
    {
        return Ok("welcome");
    }
}
