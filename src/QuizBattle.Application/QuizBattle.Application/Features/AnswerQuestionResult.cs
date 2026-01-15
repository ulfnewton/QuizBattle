using QuizBattle.Domain;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.AnswerQuestion;

public sealed record AnswerQuestionCommand(int SessionId, string QuestionCode, string SelectedChoiceCode);

public sealed record AnswerQuestionResponse(Guid SessionId, IReadOnlyList<Question> Questions);

public class AnswerQuestionResult
{
    private readonly IQuestionRepository _questions;
    private readonly ISessionRepository _sessions;
    
    public AnswerQuestionResult(IQuestionRepository q, ISessionRepository s)
    {
        _questions = q;
        _sessions = s;
    }

    public async Task<AnswerQuestionResponse> HandleAsync(AnswerQuestionCommand cmd, CancellationToken ct)
    {
        
    }
}