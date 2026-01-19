using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

/// <summary>
/// Result: What the application returns to caller (UI-agnostic)
/// </summary>
/// <param name="SessionId"></param>
/// <param name="Questions"></param>
public sealed record StartSessionResponse(Guid SessionId, IReadOnlyList<Question> Questions);