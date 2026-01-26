using System.Xml;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.FinishSession;

public class FinishQuizHandler
{
    private readonly ISessionRepository _sessionRepository;
    public FinishQuizHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<FinishQuizResult> Handle(FinishQuizCommand cmd, CancellationToken ct)
    {
        var session = await _sessionRepository.GetByIdAsync(cmd.sessionId, ct) 
                      ?? throw new InvalidOperationException("Session not found");
        
        session.Finish(DateTime.UtcNow);
        
        await _sessionRepository.UpdateAsync(session, ct);
        
        return new FinishQuizResult(session.QuestionCount,
            session.Answers.Count(a => a.IsCorrect),
            session.Score);
    }
    
}