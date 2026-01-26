namespace QuizBattle.Application.Features.FinishSession;

/// <summary>
/// Handles the completion of an ongoing quiz session by finalizing session data
/// and updating the repository.
/// </summary>
public class FinishSessionHandler
{
    private readonly ISessionRepository _sessionRepository;

    public FinishSessionHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    /// <summary>
    /// Handles the completion of an ongoing quiz session by finalizing the session data
    /// and updating the session repository.
    /// </summary>
    /// <returns>Returns the outcome of the finished session,
    /// including score, number of questions answered and time.</returns>
    public async Task<FinishSessionResult> Handle(FinishSessionCommand cmd, CancellationToken ct)
    {
        var session = await _sessionRepository.GetByIdAsync(cmd.SessionId, ct) 
                      ?? throw new Exception("Session not found");

        var finishedAt = DateTime.UtcNow;
        session.Finish(finishedAt);
        
        await _sessionRepository.UpdateAsync(session, ct);

        return new FinishSessionResult(session.Score, session.Answers.Count, finishedAt);
    }
}