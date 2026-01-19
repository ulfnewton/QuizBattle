using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartSession
{
    public sealed class StartQuizHandler
    {
        private readonly IQuestionRepository _questions;
        private readonly ISessionRepository _sessions;

        public StartQuizHandler(IQuestionRepository questions, ISessionRepository sessions)
        {
            _questions = questions;
            _sessions = sessions;
        }

        public async Task<StartQuizResult> Handle(StartQuizCommand cmd, CancellationToken ct)
        {
            var session = QuizSession.Create(cmd.QuestionCount);

            var category = string.IsNullOrWhiteSpace(cmd.Category) ? null : cmd.Category;
            var difficulty = cmd.Difficulty;

            var questions = await _questions.GetRandomAsync(
                category,
                difficulty,
                cmd.QuestionCount,
                ct);

            await _sessions.SaveAsync(session, ct);

            return new StartQuizResult(session.Id, questions);
        }
    }
}
