using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features;

public static class AnswerQuestion
{

    public sealed record AnswerQuestionCommand(Guid SessionId, string QuestionCode, string SelectedChoiceCode);


    public sealed record AnswerQuestionResult(bool IsCorrect, int CurrentScore);


    public sealed class AnswerQuestionHandler
    {
        private readonly ISessionRepository _sessions;
        private readonly IQuestionRepository _questions;

        public AnswerQuestionHandler(
            ISessionRepository sessions,
            IQuestionRepository questions)
        {
            _sessions = sessions;
            _questions = questions;
        }

        public async Task<AnswerQuestionResult> Handle(
            AnswerQuestionCommand cmd,
            CancellationToken ct = default)
        {
            var session = await _sessions.GetByIdAsync(cmd.SessionId, ct)
                          ?? throw new DomainException("Sessionen hittades inte.");

            var question = await _questions.GetByCodeAsync(cmd.QuestionCode, ct)
                           ?? throw new DomainException("Frågan hittades inte.");

            // Domain owns validation & correctness
            session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);

            await _sessions.UpdateAsync(session, ct);

            // Read outcome from domain state
            var isCorrect = session.Answers
                .Last()
                .IsCorrect;

            return new AnswerQuestionResult(
                IsCorrect: isCorrect,
                CurrentScore: session.Score);
        }
    }
}