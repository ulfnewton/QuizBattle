using QuizBattle.Application.Features.AnswerQuestion;
using QuizBattle.Application.Features.FinishQuiz;
using QuizBattle.Application.Features.StartQuiz;

namespace QuizBattle.Application.Interfaces
{
    public interface ISessionService
    {
        Task<AnswerQuestionResult> AnswerAsync(Guid sessionId, string questionCode, string selectedChoiceCode, CancellationToken ct = default);
        Task<FinishQuizResult> FinishAsync(Guid sessionId, CancellationToken ct = default);
        Task<StartQuizResult> StartAsync(int questionCount, string? category = null, int? difficulty = null, CancellationToken ct = default);
    }
}