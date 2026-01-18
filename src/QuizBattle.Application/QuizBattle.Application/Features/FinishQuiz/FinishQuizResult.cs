using System;

namespace QuizBattle.Application.Features.FinishSession;

public sealed class FinishQuizResult
{
    public int TotalScore { get; }
    public int TotalQuestions { get; }
    public object Score { get; set; }
    public object AnsweredCount { get; set; }

    public FinishQuizResult(int totalScore, int totalQuestions)
	{
        // Only outdata?
        // Assign the total score and total questions to the properties
        TotalScore = totalScore;
        TotalQuestions = totalQuestions;
    }
}
