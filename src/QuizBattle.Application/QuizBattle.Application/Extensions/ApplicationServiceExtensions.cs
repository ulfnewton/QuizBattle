namespace QuizBattle.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring application services in the dependency injection container.
    /// </summary>
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Handlers
            services.AddSingleton<StartSessionHandler>();
            services.AddSingleton<AnswerQuestionHandler>();
            services.AddSingleton<FinishSessionHandler>();

            // Services
            services.AddSingleton<IQuestionService, QuestionService>();
            services.AddSingleton<ISessionService, SessionService>();

            return services;
        }
    }
}
