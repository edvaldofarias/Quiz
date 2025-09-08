using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.WebApi.Controllers;

public class SubjectController : MainController
{
    [HttpGet]
    [Route("initials")]
    [AllowAnonymous]
    public IActionResult GetSubjectInitials()
    {
        var initials = new[] { "M", "S", "H", "G", "E" }; 
        return Ok(new { Initials = initials });
    }
    
    [HttpGet]
    [Route("names")]
    [AllowAnonymous]
    public IActionResult GetSubjectNamesByInitials(string initial)
    {
        var names = initial.ToUpper() switch
        {
            "M" => new[] { "Mathematics", "Music", "Marketing" },
            "S" => new[] { "Science", "Sociology", "Statistics" },
            "H" => new[] { "History", "Health", "Humanities" },
            "G" => new[] { "Geography", "Geology", "Genetics" },
            "E" => new[] { "English", "Economics", "Engineering" },
            _ => Array.Empty<string>()
        };
        return Ok(new { Names = names });
    }

    [HttpGet]
    [Route("status")]
    [Authorize]
    public IActionResult Status()
    {
        return Ok(new { Message = "You are authorized!" });
    }
}
