using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.StartSession;

// Handler: orkestrerar flödet. Injektera portar och IClock för testbar tid.
public sealed class StartQuizHandler
{
    private readonly IQuestionRepository _questions;
    private readonly ISessionRepository _sessions;

    public StartQuizHandler(IQuestionRepository q, ISessionRepository s)
    {
        _questions = q;
        _sessions = s;
    }

    public async Task<StartQuizResponse> HandleAsync(StartQuizCommand cmd, CancellationToken ct)
    {
        // Enkel parametervalidering på application-nivå
        if (cmd.QuestionCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(cmd.QuestionCount));

        // Hämta frågor via repository-porten
        var qs = await _questions.GetRandomAsync(cmd.Category, cmd.Difficulty, cmd.QuestionCount, ct);

        // Skapa en session. För undervisning använder vi DateTime.UtcNow direkt här.
        var session = new QuizSession
        {
            Id = Guid.NewGuid(),
            StartedAtUtc = DateTime.UtcNow,
            QuestionCount = cmd.QuestionCount
        };

        // Spara sessionen (infrastruktur tar hand om lagring)
        await _sessions.SaveAsync(session, ct);

        // Returnera response till caller (UI/Adapter)
        return new StartQuizResponse(session.Id, qs);
    }
}