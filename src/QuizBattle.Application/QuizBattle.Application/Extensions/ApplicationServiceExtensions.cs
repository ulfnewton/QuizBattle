using Microsoft.Extensions.DependencyInjection;
using QuizBattle.Application.Features.AnswerQuestion;
using QuizBattle.Application.Features.FinishQuiz;
using QuizBattle.Application.Features.StartQuiz;
using QuizBattle.Application.Interfaces;
using QuizBattle.Application.Services;

namespace QuizBattle.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Handlers
            services.AddTransient<StartQuizHandler>();
            services.AddTransient<AnswerQuestionHandler>();
            services.AddTransient<FinishQuizHandler>();

            // Fasaden/service
            services.AddSingleton<IQuestionService, QuestionService>();
            services.AddSingleton<ISessionService, SessionService>();

            return services;
        }
    }
}
