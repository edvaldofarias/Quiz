namespace Quiz.WebApi.Infrastructures.Pipeline.Swagger;

[ExcludeFromCodeCoverage]
internal static class UseSwaggerApplication
{
    internal static void UseSwaggerPipeline(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) 
            return;
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz - WEBAPI V1");
            c.RoutePrefix = "swagger";
        });
    }
}