using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartSession
{
    public sealed class StartSessionHandler
    {
        private readonly IQuestionRepository _questions;
        private readonly ISessionRepository _sessions;

        public StartSessionHandler(IQuestionRepository q, ISessionRepository s)
        {
            _questions = q; 
            _sessions = s; 
        }

        public async Task<StartSessionResult> Handle(StartSessionCommand cmd, CancellationToken ct)
        {
            // Enkel parametervalidering på application-nivå
            if (cmd.QuestionCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(cmd.QuestionCount));

            // Hämta frågor via repository-porten
            var qs = await _questions.GetRandomAsync(cmd.Category, cmd.Difficulty, cmd.QuestionCount, ct);

            // Skapa en session. För undervisning använder vi DateTime.UtcNow direkt här.
            var session = new QuizSession
            {
                Id = Guid.NewGuid(),
                StartedAtUtc = DateTime.UtcNow,
                QuestionCount = cmd.QuestionCount
            };

            // Spara sessionen (infrastruktur tar hand om lagring)
            await _sessions.SaveAsync(session, ct);

            // Returnera response till caller (UI/Adapter)
            return new StartSessionResult(session.Id, qs);
        }
    }
}
