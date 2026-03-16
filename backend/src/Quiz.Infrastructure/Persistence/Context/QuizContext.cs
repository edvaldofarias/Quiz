using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quiz.Domain.Abstractions;
using Quiz.Domain.Aggregates.QuestionAggregate;
using Quiz.Domain.Aggregates.SubjectAggregate;

namespace Quiz.Infrastructure.Persistence.Context;

public class QuizContext(DbContextOptions<QuizContext> options) : DbContext(options)
{
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureAutomaticTimestamps(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ApplyTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ApplyTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void ApplyTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(entry => entry.State is EntityState.Added or EntityState.Modified)
            .Where(entry => IsTimestampedEntity(entry.Entity.GetType()));

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                var createdAt = entry.Property(nameof(BaseEntity<int>.CreatedAt));
                createdAt.CurrentValue = DateTime.UtcNow;
                createdAt.IsModified = false;

                if (entry.Property(nameof(BaseEntity<int>.UpdatedAt)) is { } updatedAt)
                {
                    updatedAt.CurrentValue = null;
                    updatedAt.IsModified = false;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(BaseEntity<int>.CreatedAt)).IsModified = false;

                if (entry.Property(nameof(BaseEntity<int>.UpdatedAt)) is { } updatedAt)
                {
                    updatedAt.CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }

    private static void ConfigureAutomaticTimestamps(ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes();
        foreach (var entityType in entityTypes)
        {
            var clrType = entityType.ClrType;
            if (!IsTimestampedEntity(clrType))
            {
                continue;
            }

            var entityBuilder = modelBuilder.Entity(clrType);
            entityBuilder.Property<DateTime>(nameof(BaseEntity<int>.CreatedAt))
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            entityBuilder.Property<DateTime?>(nameof(BaseEntity<int>.UpdatedAt))
                .IsRequired(false);
        }
    }

    private static bool IsTimestampedEntity(Type entityType)
    {
        ArgumentNullException.ThrowIfNull(entityType);
        while (entityType != typeof(object))
        {
            if (entityType.IsGenericType && entityType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
            {
                return true;
            }

            entityType = entityType.BaseType!;
        }

        return false;
    }
}
