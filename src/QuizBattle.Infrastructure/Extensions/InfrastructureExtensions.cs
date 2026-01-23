using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using QuizBattle.Application.Interfaces;
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

            services.AddSingleton<IQuestionRepository, InMemoryQuestionRepository>();
            services.AddSingleton<ISessionRepository, InMemorySessionRepository>();

            return services;
        }
    }
}
