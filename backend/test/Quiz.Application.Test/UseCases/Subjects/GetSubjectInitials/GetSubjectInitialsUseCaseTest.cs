using Quiz.Application.UseCases.Subjects.GetSubjectInitials;

namespace Quiz.Application.Test.UseCases.Subjects.GetSubjectInitials;

[Trait("GetSubjectInitialsUseCase", "Unit")]
public class GetSubjectInitialsUseCaseTest
{
    [Fact]
    public async Task HandleAsync_Returns_Uppercase_Initials_With_One_Character_Each()
    {
        // Arrange
        var useCase = new GetSubjectInitialsUseCase();

        // Act
        var result = (await useCase.HandleAsync(CancellationToken.None)).ToList();

        // Assert
        result.Count.ShouldBe(13);
        result.ShouldAllBe(initial => initial.Length == 1 && initial == initial.ToUpperInvariant());
        result.ShouldBe(new[] { "M", "S", "E", "H", "G", "B", "C", "P", "C", "A", "M", "P", "L" });
    }
}
