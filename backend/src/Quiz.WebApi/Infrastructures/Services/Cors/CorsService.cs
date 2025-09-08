namespace Quiz.WebApi.Infrastructures.Services.Cors;

[ExcludeFromCodeCoverage]
internal static class CorsService
{
    //TODO: Review CORS policy for production
    internal static void AddCorsService(this IServiceCollection services, IWebHostEnvironment environment)
    {
        if(environment.IsProduction())
            throw new InvalidOperationException("This service is not recommended.");
        
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
    }
}