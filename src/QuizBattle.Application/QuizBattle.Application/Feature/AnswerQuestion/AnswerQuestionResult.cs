namespace QuizBattle.Application.Features.AnswerQuestion
{
    public record AnswerQuestionResult(bool Success, Guid SessionId, string QuestionCode, bool IsCorrect, string Error = null)
    {
        public static AnswerQuestionResult Fail(Guid sessionId, string questionCode, string error) =>
            new AnswerQuestionResult(false, sessionId, questionCode, false, error);
    }
}
