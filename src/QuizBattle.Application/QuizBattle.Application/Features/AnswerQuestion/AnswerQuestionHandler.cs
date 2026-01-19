using QuizBattle.Application.Features.StartSession;
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public sealed class AnswerQuestionHandler
    {
        private readonly IQuestionRepository _questions;
        private readonly ISessionRepository _sessions;

        public AnswerQuestionHandler(IQuestionRepository q, ISessionRepository s)
        {
            _questions = q ?? throw new ArgumentNullException(nameof(q));
            _sessions = s ?? throw new ArgumentNullException(nameof(s));
        }

        public Task<AnswerQuestionResult> Handle(AnswerQuestionCommand cmd, CancellationToken ct = default)
            => HandleAsync(cmd, ct);

        // Wrapper-metod
        public async Task<AnswerQuestionResult> HandleAsync(AnswerQuestionCommand cmd, CancellationToken ct)
        {
            // Enkel parametervalidering på application-nivå
            if (cmd.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId must not be empty.", nameof(cmd.SessionId));

            if (string.IsNullOrWhiteSpace(cmd.QuestionCode))
                throw new ArgumentException("QuestionCode must not be empty.", nameof(cmd.QuestionCode));

            if (string.IsNullOrWhiteSpace(cmd.SelectedChoiceCode))
                throw new ArgumentException("SelectedChoiceCode must not be empty.", nameof(cmd.SelectedChoiceCode));
            
            // Hämta session
            var session = await _sessions.GetByIdAsync(cmd.SessionId, ct);
            if (session is null)
                throw new DomainException($"Session '{cmd.SessionId}' hittades inte.");

            // Hämta fråga
            var question = await _questions.GetByCodeAsync(cmd.QuestionCode, ct);
            if (question is null)
                throw new DomainException($"Fråga '{cmd.QuestionCode}' hittades inte.");

            // Registrera svaret i Domain
            session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);

            // Uppdatera session
            await _sessions.UpdateAsync(session, ct);

            // Avgör om svaret är korrekt
            var isCorrect = question.IsCorrect(cmd.SelectedChoiceCode);

            // Return response
            return new AnswerQuestionResult(
                session.Id,
                question.Code,
                cmd.SelectedChoiceCode,
                isCorrect,
                session.Score,
                session.Answers.Count,
                session.QuestionCount,
                session.FinishedAtUtc is not null
            );

        }
    }
}
