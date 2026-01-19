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

        // Använd domänmodellens metod för att registrera svaret
        session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);

        await _sessions.UpdateAsync(session, ct);

        // Hämta det senaste svaret för att avgöra om det var korrekt
        var lastAnswer = session.Answers[session.Answers.Count - 1];
        return new AnswerQuestionResult(lastAnswer.IsCorrect, session.Score, session.Answers.Count);
    }
}
