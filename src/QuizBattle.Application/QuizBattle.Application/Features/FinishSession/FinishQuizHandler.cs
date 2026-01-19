using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    public sealed class FinishQuizHandler
    {
        private readonly ISessionRepository _sessions;

        public FinishQuizHandler(ISessionRepository sessions)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
        }

        // Wrapper-metod
        public Task<FinishQuizResult> Handle(FinishQuizCommand cmd, CancellationToken ct = default)
            => HandleAsync(cmd, ct);

        public async Task<FinishQuizResult> HandleAsync(FinishQuizCommand cmd, CancellationToken ct)
        {
            // Enkel parametervalidering på application-nivå
            if (cmd.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId must not be empty.", nameof(cmd.SessionId));

            // Hämta session
            var session = await _sessions.GetByIdAsync(cmd.SessionId, ct);
            if (session is null)
                throw new DomainException($"Session '{cmd.SessionId}' hittades inte.");

            // Avsluta session i Domain
            session.Finish(DateTime.UtcNow);

            // Uppdatera session (slutresultat)
            await _sessions.UpdateAsync(session, ct);

            // Returnera slutresultat
            return new FinishQuizResult(
                session.Id,
                session.Score,
                session.Answers.Count,
                session.QuestionCount,
                session.StartedAtUtc,
                session.FinishedAtUtc!.Value
            );
        }
    }
}
