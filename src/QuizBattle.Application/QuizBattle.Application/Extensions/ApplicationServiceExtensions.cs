using Microsoft.Extensions.DependencyInjection;
using QuizBattle.Application.Features;
using QuizBattle.Application.Interfaces;
using QuizBattle.Application.Services;

namespace QuizBattle.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IQuestionService, QuestionService>();
            services.AddSingleton<ISessionService, SessionService>();

            
            services.AddTransient<StartSession.StartQuizHandler>();
            services.AddTransient<AnswerQuestion.AnswerQuestionHandler>();
            services.AddTransient<FinishSession.FinishQuizHandler>();
            
            return services;
        }
    }
}
