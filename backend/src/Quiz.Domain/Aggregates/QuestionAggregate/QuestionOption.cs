using System.Diagnostics.CodeAnalysis;
using Quiz.Domain.Abstractions;

namespace Quiz.Domain.Aggregates.QuestionAggregate;

public class QuestionOption : BaseEntity<long>
{
    [ExcludeFromCodeCoverage]
    [Obsolete("Only for ORM", true)]
    private QuestionOption() { }
    public QuestionOption(Question question, string content, bool isCorrect, int order)
    {
        Question = question ?? throw new ArgumentNullException(nameof(question));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        IsCorrect = isCorrect;
        Order = order > 0 ? order : throw new ArgumentOutOfRangeException(nameof(order), "Order must be non-negative or zero.");
        MarkCreatedNow();
    }

    public long QuestionId { get; private set; }
    public Question Question { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public bool IsCorrect { get; private set; }
    public int Order { get; private set; }
}