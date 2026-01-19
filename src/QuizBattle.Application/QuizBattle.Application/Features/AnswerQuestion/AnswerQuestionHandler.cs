using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

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

        public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand command, CancellationToken ct = default)
        {
            if (command is null) throw new ArgumentNullException(nameof(command));

            if (command.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId får inte vara tomt", nameof(command.SessionId));

            if (string.IsNullOrWhiteSpace(command.QuestionCode))
                throw new ArgumentException($"Fråga med kod '{command.QuestionCode}' hittades inte.", nameof(command.QuestionCode));

            if (string.IsNullOrWhiteSpace(command.SelectedChoiceCode))
                throw new ArgumentException($"Vald svarsalternativkod får inte vara tom.", nameof(command.SelectedChoiceCode));

            var session = await _sessions.GetByIdAsync(command.SessionId, ct)
                ?? throw new ArgumentException($"Quiz-session med id '{command.SessionId}' hittades inte.", nameof(command.SessionId));

            var question = await _questions.GetByCodeAsync(command.QuestionCode, ct)
                ?? throw new ArgumentException($"Fråga med kod '{command.QuestionCode}' hittades inte.", nameof(command.QuestionCode));
            
            session.SubmitAnswer(question, command.SelectedChoiceCode, DateTime.UtcNow);

            await _sessions.UpdateAsync(session, ct);

            var isCorrect = question.IsCorrect(command.SelectedChoiceCode);
            return new AnswerQuestionResult(isCorrect, session.Score);
        }
    }
}