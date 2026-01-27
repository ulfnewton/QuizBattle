using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using QuizBattle.Application.Interfaces;
using QuizBattle.Infrastructure.Data;
using QuizBattle.Infrastructure.Repositories;

namespace QuizBattle.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            // Konfigurera så att DbContext använder quizbattle.
            services.AddDbContext<QuizBattleDbContext>(optionsBuilder =>
                optionsBuilder.UseSqlite("Data Source=quizbattle.db"));

            services.AddScoped<IQuestionRepository, EFCoreQuestionRepository>();
            services.AddScoped<ISessionRepository, InMemorySessionRepository>();

            return services;
        }

        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider, CancellationToken ct = default)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QuizBattleDbContext>();
            await QuizBattleDbSeeder.SeedAsync(context, ct);
        }
    }
}
