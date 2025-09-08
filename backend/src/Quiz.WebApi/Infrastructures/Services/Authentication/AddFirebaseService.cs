using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Quiz.WebApi.Infrastructures.Services.Authentication;

[ExcludeFromCodeCoverage]
internal static class AddFirebaseService
{
    internal static void AddFirebaseAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = GetApiKey(configuration);
        const string firebaseUrl = "https://securetoken.google.com";
        var authority = $"{firebaseUrl}/{apiKey}";
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authority,
                ValidateAudience = true,
                ValidAudience = apiKey,
                ValidateLifetime = true
            };
        });
    }

    private static string GetApiKey(IConfiguration configuration)
    {
        var apiKey = configuration.GetSection("Firebase:ApiKey").Value ??
                     throw new NullReferenceException("GoogleClientId is null");
        return apiKey;
    }
}