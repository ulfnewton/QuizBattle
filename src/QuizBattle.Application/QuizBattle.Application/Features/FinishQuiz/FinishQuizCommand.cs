namespace DefaultNamespace;

public class FinishQuizCommand
{
    public Guid QuizId { get; }

    public FinishQuizCommand(Guid quizId)
    {
        QuizId = quizId;
    }
}