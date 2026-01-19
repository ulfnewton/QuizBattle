using System;

namespace QuizBattle.Application.Features.AnswerQuestion;
public sealed class AnswerQuestionResult
{
    // Property indicating if the answer was correct
    public bool IsCorrect { get; } // Boolean property to indicate correctness

    public AnswerQuestionResult(bool isCorrect)
	{
        // Assign the correctness of the answer to the property
        IsCorrect = isCorrect;
    }
}
