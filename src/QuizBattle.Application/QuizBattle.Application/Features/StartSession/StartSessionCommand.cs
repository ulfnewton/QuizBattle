namespace QuizBattle.Application.Features.StartSession;

/// <summary>
/// Command: Describes input for use-case
/// </summary>
/// <param name="QuestionCount"></param>
/// <param name="Category"></param>
/// <param name="Difficulty"></param>
public sealed record StartSessionCommand(int QuestionCount, string? Category = null, int? Difficulty = null);