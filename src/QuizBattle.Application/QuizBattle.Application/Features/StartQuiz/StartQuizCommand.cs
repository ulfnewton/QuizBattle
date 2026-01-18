using System;

namespace QuizBattle.Application.Features.StartSession;

public sealed class StartQuizCommand
{
    private int? difficulty;

    // sealed class?
    public int NumberOfQuestions { get; } // Varför bara get?

    public StartQuizCommand(int numberofQuestions, string? category)
	{
		// User start a new quiz (only in data)
		NumberOfQuestions = numberofQuestions;
    }

    public StartQuizCommand(int numberofQuestions, string? category, int? difficulty) : this(numberofQuestions, category)
    {
        this.difficulty = difficulty;
    }
}
