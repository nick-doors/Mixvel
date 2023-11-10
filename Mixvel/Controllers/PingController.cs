using Microsoft.AspNetCore.Mvc;

namespace Mixvel.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    [HttpGet(Name = "Ping")]
    public ActionResult Get(
        CancellationToken cancellationToken)
    {
        return Ok();
    }
}