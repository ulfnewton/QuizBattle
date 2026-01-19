using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBattle.Domain;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.FinishSession
{
    public sealed class FinishQuizHandler
    {
        private readonly ISessionRepository _sessions;

        public FinishQuizHandler(ISessionRepository sessions)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
        }

        public async Task<FinishQuizResult> Handle(FinishQuizCommand command, CancellationToken ct = default)
        {
            if (command is null) throw new ArgumentNullException(nameof(command));

            if (command.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId får inte vara tomt.", nameof(command.SessionId));

            var session = await _sessions.GetByIdAsync(command.SessionId, ct)
                ?? throw new ArgumentException($"Session med id '{command.SessionId}' hittades inte.", nameof(command.SessionId));

            session.Finish(DateTime.UtcNow);

            await _sessions.UpdateAsync(session, ct);

            var answeredCount = session.Answers.Count;
            var correctCount = session.Score;
            var incorrectCount = answeredCount - correctCount;

            var finishedAtUtc = session.FinishedAtUtc ?? DateTime.UtcNow;

            return new FinishQuizResult(
                  session.Id,
                session.Score,
                answeredCount,
                correctCount,
                incorrectCount,
                finishedAtUtc
            ); 
        }
    }
}