using QuizBattle.Application.Features.AnswerQuestion;
using QuizBattle.Application.Features.FinishSession;
using QuizBattle.Application.Features.StartSession;

namespace QuizBattle.Application.Interfaces
{
    public interface ISessionService
    {
        Task<AnswerQuestionResult> AnswerAsync(Guid sessionId, string questionCode, string selectedChoiceCode, CancellationToken ct = default);
        Task<FinishSessionResult> FinishAsync(Guid sessionId, CancellationToken ct = default);
        Task<StartSessionResult> StartAsync(int questionCount, string? category = null, int? difficulty = null, CancellationToken ct = default);
    }
}