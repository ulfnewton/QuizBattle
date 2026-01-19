using QuizBattle.Domain;
namespace DefaultNamespace;

public class AnswerQuestionHandler
{
    public AnswerQuestionResult Handle(AnswerQuestionCommand command)
    {
        var quiz = Quiz.Load(command.QuizId);
        var isCorrect = quiz.Answer(command.Answer);

        return new AnswerQuestionResult(isCorrect);
    }
}