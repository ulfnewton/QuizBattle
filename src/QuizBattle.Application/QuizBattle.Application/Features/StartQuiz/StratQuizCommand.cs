namespace DefaultNamespace;

public class StratQuizCommand
{
    public int NumberOfQuestions { get; }

    public StartQuizCommand(int numberOfQuestions)
    {
        NumberOfQuestions = numberOfQuestions;
    }
}