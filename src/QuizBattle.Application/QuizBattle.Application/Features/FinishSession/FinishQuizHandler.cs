using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.FinishSession;

public class FinishQuizHandler
{
    private readonly ISessionRepository _sessions;

    public FinishQuizHandler(ISessionRepository sessions)
    {
        _sessions = sessions;
    }

    public async Task<FinishQuizResult> HandleAsync(FinishQuizCommand cmd, CancellationToken ct = default)
    {
        var session = await _sessions.GetByIdAsync(cmd.SessionId, ct)
            ?? throw new ArgumentException("Session not found");

        var now = DateTime.UtcNow;
        session.Finish(now);

        await _sessions.UpdateAsync(session, ct);

        return new FinishQuizResult(session.Score, session.QuestionCount, now);
    }
}
