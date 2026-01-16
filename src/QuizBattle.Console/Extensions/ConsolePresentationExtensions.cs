namespace QuizBattle.Console.Extensions
{
    public static class ConsolePresentationExtensions
    {
        public static IServiceCollection AddConsolePresentation(this IServiceCollection services)
        {
            services.AddSingleton<IConsoleQuestionPresenter, ConsoleQuestionPresenter>();

            return services;
        }
    }
}
