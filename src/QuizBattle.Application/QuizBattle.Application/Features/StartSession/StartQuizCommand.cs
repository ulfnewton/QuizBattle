namespace QuizBattle.Application.Features.StartSession;

public class StartQuizCommand
{
    public int NumberOfQuestions { get; }
    public string? Category { get; }
    public int? Difficulty { get; }

    public StartQuizCommand(int numberOfQuestions, string? category, int? difficulty)
    {
        NumberOfQuestions = numberOfQuestions;
        Category = category;
        Difficulty = difficulty;
    }
}