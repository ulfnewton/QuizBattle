using QuizBattle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public sealed class AnswerQuestionHandler
    {
        private readonly ISessionRepository _sessions;
        private readonly IQuestionRepository _questions;

        public AnswerQuestionHandler(ISessionRepository sessions, IQuestionRepository questions)
        {
            _sessions = sessions;
            _questions = questions;
        }

        public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand cmd, CancellationToken ct)
        {
            var session = await _sessions.GetByIdAsync(cmd.SessionId, ct)
                ?? throw new InvalidOperationException("Session not found.");

            var question = await _questions.GetByCodeAsync(cmd.QuestionCode, ct)
                ?? throw new InvalidOperationException("Question not found.");

            session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);

            await _sessions.UpdateAsync(session, ct);

            var lastAnswer = session.Answers.Last();

            return new AnswerQuestionResult(
                lastAnswer.IsCorrect,
                session.Answers.Count,
                session.Score);
        }
    }
}
