using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features;

// Command: enkel DTO som beskriver input för use-caset
public sealed record StartQuizCommand(int QuestionCount, string? Category = null, int? Difficulty = null);

// Result: vad application returnerar till caller (UI-agnostiskt)
public sealed record StartQuizResult(Guid SessionId, IReadOnlyList<Question> Questions);

// Handler: orkestrerar flödet. Injektera portar och IClock för testbar tid.
public sealed class StartQuizHandler  // RENAMED from StartQuizResult
{
    private readonly IQuestionRepository _questions;
    private readonly ISessionRepository _sessions;

    public StartQuizHandler(IQuestionRepository q, ISessionRepository s)
    {
        _questions = q;
        _sessions = s;
    }

    public async Task<StartQuizResult> Handle(StartQuizCommand cmd, CancellationToken ct)  // Returns StartQuizResult
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

        // Returnera result till caller (UI/Adapter)
        return new StartQuizResult(session.Id, qs);
    }
}
