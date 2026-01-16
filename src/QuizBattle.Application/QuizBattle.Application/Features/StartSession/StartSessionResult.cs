namespace QuizBattle.Application.Features.StartSession;

/// <summary>
/// The result of starting a quiz session, including the session ID
/// and the list of questions that belong to the session.
/// </summary>
public sealed record StartSessionResult(Guid SessionId, IReadOnlyList<Question> Questions);