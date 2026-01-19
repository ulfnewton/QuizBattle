namespace QuizBattle.Application.Features.AnswerQuestion;

public class AnswerQuestionResult
{
    public bool IsCorrect { get; }

    public AnswerQuestionResult(bool isCorrect)
    {
        IsCorrect = isCorrect;
    }
}