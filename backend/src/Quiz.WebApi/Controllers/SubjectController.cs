using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.UseCases.Subjects.GetSubjectInitials;
using Quiz.Application.UseCases.Subjects.GetSubjectNamesByInitials;

namespace Quiz.WebApi.Controllers;

public class SubjectController : MainController
{
    [HttpGet]
    [Route("initials")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSubjectInitials(
        [FromServices] GetSubjectInitialsUseCase useCase, CancellationToken cancellationToken)
    {
        var initials = await useCase.HandleAsync(cancellationToken);
        return Ok(new {Initials = initials});
    }

    [HttpGet]
    [Route("names")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSubjectNamesByInitials(
        [FromQuery] string initial,
        [FromServices] GetSubjectNamesByInitialsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var names = await useCase.HandleAsync(initial, cancellationToken);
        return Ok(new {Names = names});
    }

    [HttpGet]
    [Route("status")]
    [Authorize]
    public IActionResult Status()
    {
        return Ok(new {Message = "You are authorized!"});
    }
}