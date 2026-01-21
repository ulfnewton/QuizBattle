using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession
{
    public record StartQuizResult(bool Success, Guid SessionId, List<Question> Questions, string Error = null)
    {
        public static StartQuizResult Fail(string error) =>
            new StartQuizResult(false, Guid.Empty, new List<Question>(), error);
    }
}
