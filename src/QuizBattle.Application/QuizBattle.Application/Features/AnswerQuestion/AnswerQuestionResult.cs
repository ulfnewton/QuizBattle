using System;

public sealed class AnswerQuestionResult
{
    // Property indicating if the answer was correct
    public bool IsCorrect { get; }

    public AnswerQuestionResult(bool isCorrect)
	{
        // Assign the correctness of the answer to the property
        IsCorrect = isCorrect;
    }
}
