using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.FinishSession;

// Command: input för use-caset
public sealed record FinishQuizCommand(Guid SessionId);

// Result: vad application returnerar till caller
public sealed record FinishQuizResult(Guid SessionId, int FinalScore, int TotalQuestions, DateTime StartedAtUtc, DateTime FinishedAtUtc);

// Handler: orkestrerar flödet
public sealed class FinishQuizHandler
{
    private readonly ISessionRepository _sessions;
    
    public FinishQuizHandler(ISessionRepository s)
    {
        _sessions = s;
    }

    public async Task<FinishQuizResult> Handle(FinishQuizCommand cmd, CancellationToken ct)
    {
        // Hämta sessionen
        var session = await _sessions.GetByIdAsync(cmd.SessionId, ct);
        if (session == null)
            throw new InvalidOperationException($"Session {cmd.SessionId} not found");

        // Markera som avslutad
        session.FinishedAtUtc = DateTime.UtcNow;
        
        await _sessions.SaveAsync(session, ct);

        return new FinishQuizResult(
            session.Id, 
            session.Score, 
            session.QuestionCount,
            session.StartedAtUtc,
            session.FinishedAtUtc.Value
        );
    }
}
