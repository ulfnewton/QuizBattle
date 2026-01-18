using QuizBattle.Domain;
namespace DefaultNamespace;

public class FinishQuizHandler
{
    public FinishQuizResult Handle(FinishQuizCommand command)
    {
        var quiz = Quiz.Load(command.QuizId);
        var score = quiz.Finish();

        return new FinishQuizResult(score);
    }
}