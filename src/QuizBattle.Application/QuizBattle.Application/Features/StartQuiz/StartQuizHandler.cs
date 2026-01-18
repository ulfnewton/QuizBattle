using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartQuiz
{
    public sealed class StartQuizHandler
    {
        private readonly IQuestionRepository _questions;
        private readonly ISessionRepository _sessions;

        public StartQuizHandler(
            IQuestionRepository questions,
            ISessionRepository sessions)
        {
            _questions = questions;
            _sessions = sessions;
        }

        public async Task<StartQuizResult> HandleAsync(
        StartQuizCommand command,
        CancellationToken ct)
        {
            if (command.QuestionCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(command.QuestionCount));

            var questions = await _questions.GetRandomAsync(
                command.Category,
                command.Difficulty,
                command.QuestionCount,
                ct);

            var session = new QuizSession
            {
                Id = Guid.NewGuid(),
                StartedAtUtc = DateTime.UtcNow,
                QuestionCount = command.QuestionCount
            };

            await _sessions.SaveAsync(session, ct);

            return new StartQuizResult(session.Id, questions);
        }
    }
}
