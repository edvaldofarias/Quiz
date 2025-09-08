using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Quiz.WebApi.Infrastructures.Services.Swagger;

[ExcludeFromCodeCoverage]
internal static class SwaggerService
{
    internal static void AddSwagger(this IServiceCollection services)
    {
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", 
                new OpenApiInfo
                {
                    Title = "Quiz - WEBAPI", 
                    Version = "v1"
                });
            c.AddSecurityDefinition("Bearer", GetOpenApiSecurityScheme());
            c.AddSecurityRequirement(GetOpenApiSecurityRequirement());
            c.AddSwaggerXmlComments();
        }); 
    }
    
    private static OpenApiSecurityScheme GetOpenApiSecurityScheme()
    {
        return new OpenApiSecurityScheme
        {
            Scheme = "bearer",
            BearerFormat = "JWT",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Informe **apenas o token** (sem 'Bearer ').",
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        };
    }

    private static OpenApiSecurityRequirement GetOpenApiSecurityRequirement()
    {
        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                },
                Array.Empty<string>()
            }
        };
    }

    private static void AddSwaggerXmlComments(this SwaggerGenOptions swaggerGenOptions)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            swaggerGenOptions.IncludeXmlComments(xmlPath);
    }
}