namespace Quiz.Application.UseCases.Subjects.GetSubjectNamesByInitials;

public class GetSubjectNamesByInitialsUseCase
{
    public Task<IEnumerable<string>> HandleAsync(string initial, CancellationToken cancellationToken)
    {
        var names = initial.ToUpper() switch
        {
            "M" => new[] { "Mathematics", "Music", "Marketing" },
            "S" => new[] { "Science", "Sociology", "Statistics" },
            "H" => new[] { "History", "Health", "Humanities" },
            "G" => new[] { "Geography", "Geology", "Genetics" },
            "E" => new[] { "English", "Economics", "Engineering" },
            _ => Array.Empty<string>()
        };
        return Task.FromResult(names.AsEnumerable());
    }
}