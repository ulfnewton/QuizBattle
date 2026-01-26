namespace QuizBattle.Application.Features.StartSession;

/// <summary>
/// Command to initiate a quiz session. The command specifies the
/// parameters such as the number of questions, category, and difficulty.
/// </summary>
public sealed record StartSessionCommand(int QuestionCount, string? Category = null, int? Difficulty = null);