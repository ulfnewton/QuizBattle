using QuizBattle.Domain.Entities;
namespace DefaultNamespace;

public class StartQuizResult
{
    public Guid SessionId { get; }
    public IReadOnlyList<Question> Questions { get; }

    public StartQuizResult(Guid sessionId, IReadOnlyList<Question> questions)
    {
        SessionId = sessionId;
        Questions = questions;
    }
}