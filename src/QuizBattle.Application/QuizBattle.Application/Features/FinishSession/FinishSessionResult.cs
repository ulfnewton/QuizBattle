namespace QuizBattle.Application.Features.FinishSession;

/// <summary>
/// The result of completing a quiz session in the application.
/// </summary>
public sealed record FinishSessionResult(int Score, int AnsweredCount, DateTime FinishedAtUtc);