namespace QuizBattle.Application.Features.FinishSession;

/// <summary>
/// A command to finish an ongoing session in the quiz application.
/// </summary>
public sealed record FinishSessionCommand(Guid SessionId);