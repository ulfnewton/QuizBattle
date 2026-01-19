using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public sealed record StartQuizResult(IEnumerable<Question> Questions, Guid SessionId);