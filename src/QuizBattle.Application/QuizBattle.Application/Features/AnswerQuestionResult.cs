using QuizBattle.Domain;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.AnswerQuestion;

// Command: input för use-caset
public sealed record AnswerQuestionCommand(Guid SessionId, string QuestionCode, string SelectedChoiceCode);

// Result: vad application returnerar till caller
public sealed record AnswerQuestionResult(bool IsCorrect, int CurrentScore, int QuestionsAnswered);

// Handler: orkestrerar flödet
public sealed class AnswerQuestionHandler
{
    private readonly IQuestionRepository _questions;
    private readonly ISessionRepository _sessions;
    
    public AnswerQuestionHandler(IQuestionRepository q, ISessionRepository s)
    {
        _questions = q;
        _sessions = s;
    }

    public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand cmd, CancellationToken ct)
    {
        // Hämta sessionen
        var session = await _sessions.GetByIdAsync(cmd.SessionId, ct);
        if (session == null)
            throw new InvalidOperationException($"Session {cmd.SessionId} not found");

        // Hämta frågan och validera svaret
        var question = await _questions.GetByCodeAsync(cmd.QuestionCode, ct);
        if (question == null)
            throw new InvalidOperationException($"Question {cmd.QuestionCode} not found");

        var isCorrect = question.CorrectAnswerCode == cmd.SelectedChoiceCode;
        
        // Uppdatera session (detta bör egentligen vara domänlogik)
        if (isCorrect)
            session.Score++;
        session.QuestionsAnswered++;

        await _sessions.SaveAsync(session, ct);

        return new AnswerQuestionResult(isCorrect, session.Score, session.QuestionsAnswered);
    }
}
