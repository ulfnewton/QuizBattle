using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features;

public static class FinishSession
{

    public sealed record FinishQuizCommand(Guid SessionId);


    public sealed record FinishQuizResult(int QuestionCount, int CorrectAnswers, DateTime FinishedAtUtc);


    public sealed class FinishQuizHandler
    {
        private readonly ISessionRepository _sessions;

        public FinishQuizHandler(ISessionRepository sessions)
        {
            _sessions = sessions;
        }

        public async Task<FinishQuizResult> Handle(
            FinishQuizCommand cmd,
            CancellationToken ct = default)
        {
            var session = await _sessions.GetByIdAsync(cmd.SessionId, ct)
                          ?? throw new DomainException("Sessionen hittades inte.");

            session.Finish(DateTime.UtcNow);

            await _sessions.UpdateAsync(session, ct);

            return new FinishQuizResult(
                QuestionCount: session.QuestionCount,
                CorrectAnswers: session.Score,
                FinishedAtUtc: session.FinishedAtUtc!.Value);
        }
    }
}