namespace QuizBattle.Application.Features.StartSession
{
    public record StartQuizCommand(int QuestionCount, string Category = null, int? Difficulty = null);
}
