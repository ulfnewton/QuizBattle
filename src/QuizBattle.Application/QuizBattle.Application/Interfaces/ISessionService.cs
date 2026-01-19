using QuizBattle.Application.Features;


namespace QuizBattle.Application.Interfaces
{
    public interface ISessionService
    {
        Task<AnswerQuestion.AnswerQuestionResult> AnswerAsync(Guid sessionId, string questionCode, string selectedChoiceCode, CancellationToken ct = default);
        Task<FinishSession.FinishQuizResult> FinishAsync(Guid sessionId, CancellationToken ct = default);
        Task<StartSession.StartQuizResponse> StartAsync(int questionCount, string? category = null, int? difficulty = null, CancellationToken ct = default);
    }
}