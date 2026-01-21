namespace QuizBattle.Application.Features.FinishSession
{
    public record FinishQuizResult(bool Success, Guid SessionId, int AnsweredCount, int Score, DateTime StartedAtUtc, DateTime FinishedAtUtc, string Error = null)
    {
        public static FinishQuizResult Fail(Guid sessionId, string error) =>
            new FinishQuizResult(false, sessionId, 0, 0, DateTime.MinValue, DateTime.MinValue, error);
    }
}
