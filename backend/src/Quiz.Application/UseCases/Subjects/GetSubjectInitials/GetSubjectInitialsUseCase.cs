namespace Quiz.Application.UseCases.Subjects.GetSubjectInitials;

public class GetSubjectInitialsUseCase
{
    public Task<IEnumerable<string>> HandleAsync(CancellationToken cancellationToken)
    {
        var initials = new List<string>
        {
            "MATH",
            "SCI",
            "ENG",
            "HIST",
            "GEO",
            "BIO",
            "CHEM",
            "PHYS",
            "CS",
            "ART",
            "MUS",
            "PE",
            "LANG"
        };
        
        var newList = new List<string>();
        foreach (var initial in initials)
        {
            newList.Add(initial[..1].ToUpperInvariant());
        }
        
        return Task.FromResult(newList.AsEnumerable());
    }
}