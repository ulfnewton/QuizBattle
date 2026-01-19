using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public sealed class StartQuizHandler
{
    private readonly IQuestionRepository _questions;
    private readonly ISessionRepository _sessions;

    public StartQuizHandler(IQuestionRepository questionRepository, ISessionRepository sessionRepository)
    {
        _questions = questionRepository;
        _sessions = sessionRepository;
    }

    public async Task<StartQuizResult> HandleAsync(StartQuizCommand cmd, CancellationToken ct = default)
    {
        if (cmd.QuestionCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(cmd.QuestionCount));
        
        var qs = await _questions.GetRandomAsync(cmd.Category, cmd.Difficulty, cmd.QuestionCount, ct);

        var session = new QuizSession
        {
            Id = Guid.NewGuid(),
            StartedAtUtc = DateTime.UtcNow,
            QuestionCount = cmd.QuestionCount
        };

        await _sessions.SaveAsync(session, ct);

        return new StartQuizResult(session.Id, qs);
    }

}
