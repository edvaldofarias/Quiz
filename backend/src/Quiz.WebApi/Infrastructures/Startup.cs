using Quiz.Infrastructure.DependencyInjection;
using Quiz.WebApi.Infrastructures.Pipeline.Swagger;
using Quiz.WebApi.Infrastructures.Services.Authentication;
using Quiz.WebApi.Infrastructures.Services.Cors;
using Quiz.WebApi.Infrastructures.Services.Culture;
using Quiz.WebApi.Infrastructures.Services.Swagger;

namespace Quiz.WebApi.Infrastructures;

public static class Startup
{
    internal static void AddStartup(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddCultureService();
        services.AddFirebaseAuthentication(configuration);
        services.AddSwagger();
        services.AddCorsService(environment);

        services
            .AddInfrastructure(configuration)
            .AddUseCaseServices();
    }

    internal static void UseStartup(this WebApplication app, IConfiguration configuration)
    {
        app.UseSwaggerPipeline();
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
