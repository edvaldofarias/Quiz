using Quiz.Domain.Aggregates.QuestionAggregate;

namespace Quiz.Domain.Test.Aggregates.QuestionAggregate;

[Trait("QuestionOption", "Unit")]
public class QuestionOptionTest : BaseTest
{
    [Fact]
    public void CreateQuestionOption_ShouldCreateQuestionOption_WhenParametersAreValid()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        var question = new Question(subjectId, description);
        var content = Faker.Lorem.Sentence();
        var isCorrect = Faker.Random.Bool();
        var order = Faker.Random.Int(1, 10);

        // Act
        var option = new QuestionOption(question, content, isCorrect, order);
        
        // Assert
        option.ShouldSatisfyAllConditions(() =>
        {
            option.ShouldNotBeNull();
            option.Content.ShouldBe(content);
            option.IsCorrect.ShouldBe(isCorrect);
            option.Order.ShouldBe(order);
            option.QuestionId.ShouldBe(question.Id);
            option.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            option.UpdatedAt.ShouldBeNull();
        });
    }

    [Fact]
    public void CreateQuestionOption_ShouldThrowArgumentNullException_WhenQuestionIsNull()
    {
        // Arrange
        Question question = null!;
        var content = Faker.Lorem.Sentence();
        var isCorrect = Faker.Random.Bool();
        var order = Faker.Random.Int(1, 10);

        // Act
        Action act = () => new QuestionOption(question, content, isCorrect, order);

        // Assert
        act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void CreateQuestionOption_ShouldThrowArgumentNullException_WhenContentIsNull()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        var question = new Question(subjectId, description);
        string content = null!;
        var isCorrect = Faker.Random.Bool();
        var order = Faker.Random.Int(1, 10);

        // Act
        Action act = () => new QuestionOption(question, content, isCorrect, order);
        
        // Assert
        act.ShouldThrow<ArgumentNullException>();
    }
    
    [Fact]
    public void CreateQuestionOption_ShouldThrowArgumentOutOfRangeException_WhenOrderIsZero()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        var question = new Question(subjectId, description);
        var content = Faker.Lorem.Sentence();
        var isCorrect = Faker.Random.Bool();
        var order = 0;
        
        // Act
        Action act = () => new QuestionOption(question, content, isCorrect, order);
        
        // Assert
        act.ShouldThrow<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void CreateQuestionOption_ShouldThrowArgumentOutOfRangeException_WhenOrderIsNegative()
    {
        // Arrange
        var description = Faker.Lorem.Sentence();
        var subjectId = Faker.Random.Int(1, 100);
        var question = new Question(subjectId, description);
        var content = Faker.Lorem.Sentence();
        var isCorrect = Faker.Random.Bool();
        var order = -1;

        // Act
        Action act = () => new QuestionOption(question, content, isCorrect, order);

        // Assert
        act.ShouldThrow<ArgumentOutOfRangeException>();
    }
}