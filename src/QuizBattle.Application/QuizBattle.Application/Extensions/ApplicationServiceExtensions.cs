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
            services.AddSingleton<IQuestionService, QuestionService>();
            services.AddSingleton<ISessionService, SessionService>();

            //använd ctrl+k och sedan direkt ctrl+u för att avkommentera markerad kod
            //använd ctrl+k och sedan direkt ctrl+c för att kommentera markerad kod
            services.AddScoped<StartQuizHandler>();
            services.AddScoped<AnswerQuestionHandler>();
            services.AddScoped<FinishQuizHandler>();

            return services;
        }
    }
}
