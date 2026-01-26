namespace QuizBattle.Application.Features.StartSession;

/// <summary>
/// Command to start a new quiz session.
/// </summary>
public sealed record  StartQuizCommand(int QuestionCount, string? Category = null, int? Difficulty = null);

    
