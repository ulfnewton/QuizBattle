namespace QuizBattle.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {

            services.AddSingleton<IQuestionRepository, InMemoryQuestionRepository>();
            services.AddSingleton<ISessionRepository, InMemorySessionRepository>();

            return services;
        }
    }
}
