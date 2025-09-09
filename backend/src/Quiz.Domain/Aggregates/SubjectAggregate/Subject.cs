using System.Diagnostics.CodeAnalysis;
using Quiz.Domain.Abstractions;

namespace Quiz.Domain.Aggregates.SubjectAggregate;

public class Subject: AggregateRoot<int>
{
    [ExcludeFromCodeCoverage]
    [Obsolete("Only for ORM", true)]
    private Subject() { }
    public Subject(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Subject name cannot be empty.", nameof(name));
        
        Name = name;
        Initial = Name[..1].ToUpperInvariant();
        MarkCreatedNow();
    }
    
    public string Name { get; private set; } = null!;
    public string Initial { get; private set; } = null!;
}