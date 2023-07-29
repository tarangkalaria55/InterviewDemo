using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseApiController : ControllerBase
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    //public string GetOriginFromRequest() => $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";


    //[ApiExplorerSettings(IgnoreApi = true)]
    //public string? GetIpAddress() =>
    //Request.Headers.ContainsKey("X-Forwarded-For")
    //    ? Request.Headers["X-Forwarded-For"]
    //    : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
}
