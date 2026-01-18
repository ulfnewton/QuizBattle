namespace QuizBattle.Application.Features.StartSession;


public sealed record StartQuizCommand(int questionCount, string? category, int? difficulty);