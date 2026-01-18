using System;

namespace QuizBattle.Application.Features.FinishSession;

public sealed class FinishQuizResult
{
    public object Score { get; set; }
    public object AnsweredCount { get; set; }

    public FinishQuizResult(int score, int answeredCount)
	{
        // Assign the total score and total questions to the properties    
        Score = score; 
        AnsweredCount = answeredCount; 
    }
}
