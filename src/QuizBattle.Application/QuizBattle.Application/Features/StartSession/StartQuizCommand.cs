namespace QuizBattle.Application.Features.StartSession;

public sealed record StartQuizCommand(
    int  QuestionCount,
    string? Category,
    int? Difficulty);