using System.Diagnostics.CodeAnalysis;
using Quiz.Domain.Abstractions;

namespace Quiz.Domain.Aggregates.QuestionAggregate;

public class Question : AggregateRoot<long>
{
    private readonly List<QuestionOption> _options = [];
    
    [ExcludeFromCodeCoverage]
    [Obsolete("Only for ORM", true)]
    private Question() { }
    public Question(int subjectId, string stem)
    {
        if (string.IsNullOrWhiteSpace(stem))
            throw new ArgumentException("Question stem cannot be empty.", nameof(stem));
        
        SubjectId = subjectId;
        Stem = stem;
        MarkCreatedNow();
    }

    public int SubjectId { get; private set; }
    public string Stem { get; private set; } = null!;
    public IReadOnlyCollection<QuestionOption> Options => _options.AsReadOnly();

    public void AddOption(string content, bool isCorrect, int order)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Option content cannot be empty.", nameof(content));
        if(isCorrect && _options.Any(o => o.IsCorrect))
            throw new InvalidOperationException("There is already a correct option for this question.");
        if (_options.Any(o => o.Order == order))
            throw new ArgumentException($"An option with order {order} already exists.", nameof(order));
        
        var option = new QuestionOption(this, content, isCorrect, order);
        _options.Add(option);
        MarkUpdatedNow();
    }
}