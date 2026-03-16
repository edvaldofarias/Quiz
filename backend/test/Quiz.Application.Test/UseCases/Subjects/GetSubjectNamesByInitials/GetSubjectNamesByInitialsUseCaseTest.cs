using Quiz.Application.UseCases.Subjects.GetSubjectNamesByInitials;

namespace Quiz.Application.Test.UseCases.Subjects.GetSubjectNamesByInitials;

[Trait("GetSubjectNamesByInitialsUseCase", "Unit")]
public class GetSubjectNamesByInitialsUseCaseTest
{
    [Fact]
    public async Task HandleAsync_With_Lowercase_Initial_Returns_Names_For_That_Initial()
    {
        // Arrange
        var useCase = new GetSubjectNamesByInitialsUseCase();

        // Act
        var result = (await useCase.HandleAsync("m", CancellationToken.None)).ToList();

        // Assert
        result.ShouldBe(new[] { "Mathematics", "Music", "Marketing" });
    }

    [Fact]
    public async Task HandleAsync_With_Unknown_Initial_Returns_Empty_List()
    {
        // Arrange
        var useCase = new GetSubjectNamesByInitialsUseCase();

        // Act
        var result = await useCase.HandleAsync("z", CancellationToken.None);

        // Assert
        result.ShouldBeEmpty();
    }
}
