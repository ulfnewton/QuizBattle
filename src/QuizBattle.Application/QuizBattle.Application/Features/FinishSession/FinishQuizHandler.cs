using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.FinishSession;

public class FinishQuizHandler
{
    private readonly ISessionRepository _sessionRepository;

    public FinishQuizHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
    }

    public async Task<FinishQuizResult> Handle(FinishQuizCommand command, CancellationToken ct = default)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));
        if (command.SessionId == Guid.Empty)
            throw new ArgumentException("Session-ID får inte vara tomt", nameof(command.SessionId));
        
        var session = await _sessionRepository.GetByIdAsync(command.SessionId, ct);
        if (session is null)
            throw new ArgumentException($"Session '{command.SessionId}' hittades inte", nameof(command.SessionId));
        
        session.Finish(DateTime.UtcNow);
        
        await _sessionRepository.UpdateAsync(session, ct);
        
        return new FinishQuizResult(
            totalQuestions: session.QuestionCount,
            answeredQuestions: session.Answers.Count,
            score: session.Score);
    }
}