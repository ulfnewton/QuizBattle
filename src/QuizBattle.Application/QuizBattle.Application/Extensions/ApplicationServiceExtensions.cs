using Microsoft.Extensions.DependencyInjection;
using QuizBattle.Application.Interfaces;
using QuizBattle.Application.Services;
using QuizBattle.Application.Features.StartSession;
using QuizBattle.Application.Features.AnswerQuestion;
using QuizBattle.Application.Features.FinishSession;

namespace QuizBattle.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IQuestionService, QuestionService>();
            services.AddSingleton<ISessionService, SessionService>();

            services.AddTransient<StartQuizHandler>();
            services.AddTransient<AnswerQuestionHandler>();
            services.AddTransient<FinishQuizHandler>();

            return services;
        }
    }
}