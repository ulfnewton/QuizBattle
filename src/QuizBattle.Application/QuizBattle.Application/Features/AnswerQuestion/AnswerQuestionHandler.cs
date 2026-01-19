using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBattle.Domain;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public sealed class AnswerQuestionHandler
    {
        private readonly ISessionRepository _sessions;
        private readonly IQuestionRepository _questions;

        public AnswerQuestionHandler(ISessionRepository sessions, IQuestionRepository questions)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            _questions = questions ?? throw new ArgumentNullException(nameof(questions));
        }

        public async Task<AnswerQuestionResult> HandleAsync(
                AnswerQuestionCommand cmd,
                CancellationToken ct)
        {
            if (cmd.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId must be set", nameof(cmd.SessionId));

            var session = await _sessions.GetByIdAsync(cmd.SessionId, ct);

            if (session is null)
                throw new InvalidOperationException("Session not found");

            var question = await _questions.GetByCodeAsync(cmd.QuestionCode, ct);

            if (question is null)
                throw new InvalidOperationException("Question not found");

            session.SubmitAnswer(
                question,
                cmd.SelectedChoiceCode,
                DateTime.UtcNow
            );

            var lastAnswer = session.Answers.Last();
            bool isCorrect = lastAnswer.IsCorrect;

            await _sessions.UpdateAsync(session, ct);
            return new AnswerQuestionResult(isCorrect);
        }

    }
}
