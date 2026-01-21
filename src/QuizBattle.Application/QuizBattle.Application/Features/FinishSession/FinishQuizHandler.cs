using System;
using System.Threading;
using System.Threading.Tasks;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.FinishSession;

public sealed class FinishQuizHandler
{
    private readonly ISessionRepository _sessionRepository;

    public FinishQuizHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
    }

    public async Task<FinishQuizResult> Handle(FinishQuizCommand command, CancellationToken ct = default)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));
        if (command.SessionId == Guid.Empty) throw new ArgumentException("SessionId must be set.", nameof(command.SessionId));

        var session = await _sessionRepository.GetByIdAsync(command.SessionId, ct);
        if (session is null) throw new ArgumentException($"Session '{command.SessionId}' not found.", nameof(command.SessionId));

        session.Finish(DateTime.UtcNow);

        await _sessionRepository.UpdateAsync(session, ct);

        return new FinishQuizResult(session.Score, session.Answers.Count);
    }
}