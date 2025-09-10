using Quiz.Domain.Aggregates.QuestionAggregate;

namespace Quiz.Domain.Test.Aggregates.QuestionAggregate;

[Trait("Question", "Unit")]
public class QuestionTest : BaseTest
{
    [Fact]
    public void CreateQuestion_ShouldCreateQuestion_WhenParametersAreValid()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        
        // Act
        var question = new Question(subjectId, description);

        // Assert
        question.ShouldSatisfyAllConditions(()=>
        {
            question.ShouldNotBeNull();
            question.Stem.ShouldBe(description);
            question.SubjectId.ShouldBe(subjectId);
            question.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            question.UpdatedAt.ShouldBeNull();
        });
    }

    [Fact]
    public void CreateQuestion_ShouldThrowArgumentException_WhenDescriptionIsEmpty()
    {
        // Arrange
        var subjectId = Faker.Random.Int(1, 100);
        var description = string.Empty;

        // Act
        Action act = () => new Question(subjectId, description);

        // Assert
        act.ShouldThrow<ArgumentException>();
    }

    [Fact]
    public void CreateQuestion_ShouldThrowArgumentException_WhenDescriptionIsWhitespace()
    {
        // Arrange
        var subjectId = Faker.Random.Int(1, 100);
        var description = "   ";

        // Act
        Action act = () => new Question(subjectId, description);

        // Assert
        act.ShouldThrow<ArgumentException>();
    }

    [Fact]
    public void AddOption_ShouldAddOption_WhenParametersAreValid()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        var question = new Question(subjectId, description);
        var content = Faker.Lorem.Sentence();
        var isCorrect = true;
        var order = 1;

        // Act
        question.AddOption(content, isCorrect, order);
        var option = question.Options.FirstOrDefault();
        
        // Assert
        option.ShouldSatisfyAllConditions(() =>
        {
            option.ShouldNotBeNull();
            option!.Content.ShouldBe(content);
            option.IsCorrect.ShouldBe(isCorrect);
            option.Order.ShouldBe(order);
            option.QuestionId.ShouldBe(question.Id);
            option.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            option.UpdatedAt.ShouldBeNull();
            question.UpdatedAt?.ShouldBeGreaterThanOrEqualTo(question.CreatedAt);
        });
    }

    [Fact]
    public void AddOption_ShouldThrowArgumentException_WhenContentIsEmpty()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        var question = new Question(subjectId, description);
        var content = string.Empty;
        var isCorrect = true;
        var order = 1;

        // Act
        Action act = () => question.AddOption(content, isCorrect, order);

        // Assert
        act.ShouldThrow<ArgumentException>();
    }

    [Fact]
    public void AddOption_ShouldThrowArgumentException_WhenContentIsWhitespace()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        var question = new Question(subjectId, description);
        var content = "   ";
        var isCorrect = true;
        var order = 1;

        // Act
        Action act = () => question.AddOption(content, isCorrect, order);
        
        // Assert
        act.ShouldThrow<ArgumentException>();
    }
    
    [Fact]
    public void AddOption_ShouldThrowInvalidOperationException_WhenAddingSecondCorrectOption()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        var question = new Question(subjectId, description);
        var content1 = Faker.Lorem.Sentence();
        var content2 = Faker.Lorem.Sentence();
        var order1 = 1;
        var order2 = 2;
        question.AddOption(content1, true, order1);
        
        // Act
        Action act = () => question.AddOption(content2, true, order2);
        
        // Assert
        act.ShouldThrow<InvalidOperationException>();
    }
    
    [Fact]
    public void AddOption_ShouldThrowArgumentException_WhenAddingOptionWithDuplicateOrder()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        var question = new Question(subjectId, description);
        var content1 = Faker.Lorem.Sentence();
        var content2 = Faker.Lorem.Sentence();
        var order = 1;
        question.AddOption(content1, false, order);
        
        // Act
        Action act = () => question.AddOption(content2, true, order);
        
        // Assert
        act.ShouldThrow<ArgumentException>();
    }
}