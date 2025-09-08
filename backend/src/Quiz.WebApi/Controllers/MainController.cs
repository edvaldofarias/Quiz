using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase;