using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public class StartQuizResult
{
    public IEnumerable<Question> Questions { get; set; }
    public Guid SessionId { get; set; }
}