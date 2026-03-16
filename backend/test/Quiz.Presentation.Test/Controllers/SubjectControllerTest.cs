namespace Quiz.Presentation.Test.Controllers;

[Trait("SubjectController", "Unit")]
public class SubjectControllerTest
{
    [Fact]
    public async Task GetSubjectInitials_Returns_Ok_With_Initials_Payload()
    {
        // Arrange
        var controller = new SubjectController();
        var useCase = new GetSubjectInitialsUseCase();

        // Act
        var response = await controller.GetSubjectInitials(useCase, CancellationToken.None);

        // Assert
        var okResult = response.ShouldBeOfType<OkObjectResult>();
        var initials = GetPayloadValue<IEnumerable<string>>(okResult.Value, "Initials");
        initials.ShouldNotBeNull();
        initials.ShouldContain("M");
    }

    [Fact]
    public async Task GetSubjectNamesByInitials_Returns_Ok_With_Names_Payload()
    {
        // Arrange
        var controller = new SubjectController();
        var useCase = new GetSubjectNamesByInitialsUseCase();

        // Act
        var response = await controller.GetSubjectNamesByInitials("s", useCase, CancellationToken.None);

        // Assert
        var okResult = response.ShouldBeOfType<OkObjectResult>();
        var names = GetPayloadValue<IEnumerable<string>>(okResult.Value, "Names").ToList();
        names.ShouldBe(new[] { "Science", "Sociology", "Statistics" });
    }

    [Fact]
    public void Status_Returns_Ok_With_Authorized_Message()
    {
        // Arrange
        var controller = new SubjectController();

        // Act
        var response = controller.Status();

        // Assert
        var okResult = response.ShouldBeOfType<OkObjectResult>();
        var message = GetPayloadValue<string>(okResult.Value, "Message");
        message.ShouldBe("You are authorized!");
    }

    private static T GetPayloadValue<T>(object? payload, string propertyName)
    {
        payload.ShouldNotBeNull();

        var property = payload.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        property.ShouldNotBeNull();

        var value = property.GetValue(payload);
        value.ShouldNotBeNull();

        return (T)value;
    }
}
