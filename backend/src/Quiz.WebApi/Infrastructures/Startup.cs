using Quiz.WebApi.Infrastructures.Pipeline.Swagger;
using Quiz.WebApi.Infrastructures.Services.Authentication;
using Quiz.WebApi.Infrastructures.Services.Culture;
using Quiz.WebApi.Infrastructures.Services.Swagger;

namespace Quiz.WebApi.Infrastructures;

public static class Startup
{
    internal static void AddStartup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddCultureService();
        services.AddFirebaseAuthentication(configuration);
        services.AddSwagger();
    }

    internal static void UseStartup(this WebApplication app, IConfiguration configuration)
    {
        app.UseSwaggerPipeline();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
