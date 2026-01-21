namespace QuizBattle.Application.Features.StartSession
{
    public sealed record StartQuizCommand(int QuestionCount, string? Category = null, int? Difficulty = null);
}
