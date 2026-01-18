using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public sealed record StartQuizResponse(Guid SessionId, IReadOnlyList<Question> Questions);