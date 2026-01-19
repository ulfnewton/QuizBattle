using Microsoft.Extensions.DependencyInjection;
using QuizBattle.Application.Features.AnswerQuestion;
using QuizBattle.Application.Features.FinishSession;
using QuizBattle.Application.Features.StartSession;
using QuizBattle.Application.Interfaces;
using QuizBattle.Application.Services;

namespace QuizBattle.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Facade services
            services.AddSingleton<IQuestionService, QuestionService>();
            services.AddSingleton<ISessionService, SessionService>();

            // Handlers - Had to add these too for the SessionService to work - Handlers is now registered as scoped!
            services.AddScoped<StartQuizHandler>();
            services.AddScoped<AnswerQuestionHandler>();
            services.AddScoped<FinishQuizHandler>();

            return services;
        }
    }
}
