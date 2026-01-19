namespace DefaultNamespace;

public class AnswerQuestionsCommand
{
    public Guid QuizId { get; }
    public string Answer { get; }

    public AnswerQuestionCommand(Guid quizId, string answer)
    {
        QuizId = quizId;
        Answer = answer;
    }
}