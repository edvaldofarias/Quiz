using Quiz.Domain.Aggregates.SubjectAggregate;

namespace Quiz.Domain.Test.Aggregates.SubjectAggregate;

[Trait("Subject", "Unit")]
public class SubjectTest : BaseTest
{
    [Fact]
    public void Subject_Name_Is_Null_Throws_ArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Subject(null!));
    }

    [Fact]
    public void Subject_Name_Is_Empty_Throws_ArgumentException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Subject(string.Empty));
    }

    [Fact]
    public void Subject_Name_Is_Whitespace_Throws_ArgumentException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Subject("   "));
    }

    [Fact]
    public void Subject_Creation_Sets_Properties_Correctly()
    {
        // Arrange
        var name = Faker.Commerce.Department();
        var expectedInitial = name[..1].ToUpperInvariant();

        // Act
        var subject = new Subject(name);

        // Assert
        subject.Should().NotBeNull();
        subject.Name.Should().Be(name);
        subject.Initial.Should().Be(expectedInitial);
        subject.CreatedAt.Should().BeOnOrBefore(DateTime.UtcNow);
        subject.UpdatedAt.Should().BeNull();
    }
}