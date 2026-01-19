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
        private readonly ISessionRepository _sessionRepository;
        public FinishQuizHandler(ISessionRepository sessionRepository)
            => _sessionRepository = sessionRepository;

        public async Task<FinishQuizResult> Handle(FinishQuizCommand cmd, CancellationToken ct = default)
        {
            var session = await _sessionRepository.GetByIdAsync(cmd.SessionId, ct);
            if (session == null)
                throw new ArgumentException("Session not found", nameof(cmd.SessionId));

            session.Finish(DateTime.UtcNow);

            await _sessionRepository.UpdateAsync(session, ct);

            return new FinishQuizResult(session.QuestionCount, session.Answers.Count,
                session.Answers.Count(a => a.IsCorrect), session.Score);
        }
    }
}
