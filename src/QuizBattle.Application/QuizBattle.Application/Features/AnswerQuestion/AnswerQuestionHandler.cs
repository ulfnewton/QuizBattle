using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.AnswerQuestion;

public sealed class AnswerQuestionHandler
{
    private readonly ISessionRepository _sessions;
    private readonly IQuestionRepository _questions;

    public AnswerQuestionHandler(ISessionRepository sessionRepository, IQuestionRepository questionRepository)
    {
        _sessions = sessionRepository;
        _questions = questionRepository;
    }

    public async Task<AnswerQuestionResult> HandleAsync(AnswerQuestionCommand cmd, CancellationToken ct = default)
    {
        var session = await _sessions.GetByIdAsync(cmd.SessionId, ct)
            ?? throw new ArgumentException("Session not found");

        var question = await _questions.GetByCodeAsync(cmd.QuestionCode,ct)
            ?? throw new ArgumentException("Question not found");

        session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);

        await _sessions.UpdateAsync(session, ct);

        return new AnswerQuestionResult(question.IsCorrect(cmd.SelectedChoiceCode), question.CorrectAnswerCode);
    }
}
