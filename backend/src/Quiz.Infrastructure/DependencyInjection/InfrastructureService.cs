using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Infrastructure.Persistence.Context;

namespace Quiz.Infrastructure.DependencyInjection;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("SqlServer")
                   ?? throw new InvalidOperationException("ConnectionStrings:SqlServer not configured.");

        services.AddDbContext<QuizContext>(opt =>
        {
            opt.UseSqlServer(conn, sql =>
            {
                sql.MigrationsAssembly(typeof(QuizContext).Assembly.FullName);
                sql.EnableRetryOnFailure(maxRetryCount: 5);
                sql.CommandTimeout(30);
            });

#if DEBUG
            opt.EnableSensitiveDataLogging();
            opt.EnableDetailedErrors();
#endif
        });

        return services;
    }
}