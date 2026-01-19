using QuizBattle.Application.Interfaces;
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
            _sessions = sessions;
        }

        public async Task<FinishQuizResult> Handle(FinishQuizCommand cmd, CancellationToken ct)
        {
            var session = await _sessions.GetByIdAsync(cmd.SessionId, ct)
                ?? throw new InvalidOperationException("Session not found.");

            session.Finish(DateTime.UtcNow);

            await _sessions.UpdateAsync(session, ct);

            return new FinishQuizResult(
                session.Score,
                session.Answers.Count,
                session.QuestionCount);
        }
    }
}
