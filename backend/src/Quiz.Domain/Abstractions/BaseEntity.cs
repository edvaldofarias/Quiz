using System.Diagnostics.CodeAnalysis;

namespace Quiz.Domain.Abstractions;

[ExcludeFromCodeCoverage]
public abstract class BaseEntity<T> where T : notnull
{
    protected BaseEntity() { }
    public T Id { get; protected set; } = default!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not BaseEntity<T> other) return false;
        if (EqualityComparer<T>.Default.Equals(Id, default!)) return false;
        if (EqualityComparer<T>.Default.Equals(other.Id, default!)) return false;
        return EqualityComparer<T>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode() =>
        EqualityComparer<T>.Default.Equals(Id, default!)
            ? base.GetHashCode()
            : Id.GetHashCode();
    
    protected void MarkCreatedNow() => CreatedAt = DateTime.UtcNow;
    protected void MarkUpdatedNow() => UpdatedAt = DateTime.UtcNow;
}