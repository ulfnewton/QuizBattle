using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBattle.Domain;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.FinishQuiz
{
    public sealed class FinishQuizHandler
    {
        private readonly ISessionRepository _sessions;

        public FinishQuizHandler(ISessionRepository sessions)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
        }

        public async Task<FinishQuizResult> HandleAsync(
            FinishQuizCommand cmd,
            CancellationToken ct)
        {
            if (cmd.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId must be set", nameof(cmd.SessionId));

            var session = await _sessions.GetByIdAsync(cmd.SessionId, ct)
                          ?? throw new InvalidOperationException("Session not found");

            session.Finish(DateTime.UtcNow);

            var questionStats = session.Answers
    .               ToDictionary(a => a.Question.Code, a => a.IsCorrect);

            await _sessions.UpdateAsync(session, ct);

            return new FinishQuizResult(session.Score, questionStats);
        }
    }
}
