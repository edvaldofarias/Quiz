using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Quiz.WebApi.Infrastructures;

public static class Startup
{
    internal static void AddStartup(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddControllers();

        services.AddEndpointsApiExplorer();


        services.AddSwaggerGen(c =>
        {
            // Documento básico
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz API", Version = "v1" });

            // Esquema de segurança "Bearer"
            var jwtSecurityScheme = new OpenApiSecurityScheme
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

            // Definição + Requisito global (aplica a todas as operações)
            c.AddSecurityDefinition("Bearer", jwtSecurityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        });


        // Firebase Authentication
        var apiKey = configuration.GetSection("Firebase:ApiKey").Value ?? throw new NullReferenceException("GoogleClientId is null");

        services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.Authority = $"https://securetoken.google.com/{apiKey}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"https://securetoken.google.com/{apiKey}",
                        ValidateAudience = true,
                        ValidAudience = apiKey,
                        ValidateLifetime = true
                    };
                });
    }

    internal static void UseStartup(this WebApplication app, IConfiguration configuration)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz.API v1");
                c.RoutePrefix = "swagger";
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
