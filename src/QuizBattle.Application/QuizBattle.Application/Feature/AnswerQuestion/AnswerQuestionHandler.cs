using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public class AnswerQuestionHandler
    {
        private readonly ISessionRepository _sessions;
        private readonly IQuestionRepository _questions;

        public AnswerQuestionHandler(ISessionRepository sessions, IQuestionRepository questions)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            _questions = questions ?? throw new ArgumentNullException(nameof(questions));
        }

        public async Task<AnswerQuestionResult> HandleAsync(AnswerQuestionCommand cmd, CancellationToken ct = default)
        {
            // null-check
            ArgumentNullException.ThrowIfNull(cmd, nameof(cmd));

            if (cmd.SessionId == Guid.Empty)
            {
                return AnswerQuestionResult.Fail(cmd.SessionId, cmd.QuestionCode, "SessionId får inte vara tomt.");
            }

            try
            {
                var session = await _sessions.GetByIdAsync(cmd.SessionId, ct);
                if (session == null)
                {
                    return AnswerQuestionResult.Fail(cmd.SessionId, cmd.QuestionCode, "Sessionen saknas.");
                }

                var question = await _questions.GetByCodeAsync(cmd.QuestionCode, ct);

                session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);

                await _sessions.SaveAsync(session, ct);

                return new AnswerQuestionResult(
                    true,
                    session.Id,
                    question.Code,
                    question.IsCorrect(cmd.SelectedChoiceCode)
                );
            }
            catch (DomainException ex)
            {
                // Domänfel
                return AnswerQuestionResult.Fail(cmd.SessionId, cmd.QuestionCode, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                // Valideringsfel
                return AnswerQuestionResult.Fail(cmd.SessionId, cmd.QuestionCode, ex.Message);
            }
        }
    }
}
