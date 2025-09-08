using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    [Route("index")]
    public IActionResult Index()
    {
        return Ok(new { Message = "Welcome to the Home Controller!" });
    }

    [HttpGet]
    [Route("status")]
    [Authorize]
    public IActionResult Status()
    {
        return Ok(new { Message = "You are authorized!" });
    }
}
