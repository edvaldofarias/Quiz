using Microsoft.Extensions.DependencyInjection;
using Quiz.Application.UseCases.Subjects.GetSubjectInitials;
using Quiz.Application.UseCases.Subjects.GetSubjectNamesByInitials;

namespace Quiz.Infrastructure.DependencyInjection;

public static class UseCaseService 
{
    public static IServiceCollection AddUseCaseServices(this IServiceCollection services)
    {
        services.AddScoped<GetSubjectNamesByInitialsUseCase>();
        services.AddScoped<GetSubjectInitialsUseCase>();
        return services;
    }
}