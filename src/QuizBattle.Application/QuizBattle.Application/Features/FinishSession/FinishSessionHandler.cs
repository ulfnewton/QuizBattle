using QuizBattle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    public class FinishSessionHandler
    {
        private readonly ISessionRepository _sessionRepository;

        public FinishSessionHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<FinishSessionResult> Handle(FinishSessionCommand cmd, CancellationToken ct)
        {
            var session = await _sessionRepository.GetByIdAsync(cmd.SessionId, ct);
            if (cmd.SessionId == null)
            {
                throw new ArgumentException("Session id not found!");
            }

            var finishedAt = DateTime.UtcNow;

            session.Finish(finishedAt);

            await _sessionRepository.UpdateAsync(session, ct);

            return new FinishSessionResult(session.Score, session.Answers.Count(), finishedAt);
        }
    }
   
}
