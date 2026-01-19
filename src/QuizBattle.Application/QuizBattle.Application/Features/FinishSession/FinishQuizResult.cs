namespace QuizBattle.Application.Features.FinishSession;

public class FinishQuizResult
{
    public int TotalQuestions { get; }
    public int AnsweredQuestions { get; }
    
    public int Score { get; }
    
    public int AnsweredCount => AnsweredQuestions;
    
    public FinishQuizResult(int totalQuestions, int answeredQuestions, int score)
    {
        TotalQuestions = totalQuestions;
        AnsweredQuestions = answeredQuestions;
        Score = score;
    }
}