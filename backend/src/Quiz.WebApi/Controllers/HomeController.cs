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
}
