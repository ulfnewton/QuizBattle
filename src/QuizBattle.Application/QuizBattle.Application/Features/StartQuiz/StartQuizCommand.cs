using System;

public sealed class StartQuizCommand
{
	// sealed class?
	public int NumberOfQuestions { get; } // Varför bara get?

    public StartQuizCommand(int numberofQuestions)
	{
		// User start a new quiz (only in data)
		NumberOfQuestions = numberofQuestions;
    }
}
