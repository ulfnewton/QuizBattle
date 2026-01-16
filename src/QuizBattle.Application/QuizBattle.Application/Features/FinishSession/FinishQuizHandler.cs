using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.FinishSession;

public sealed class FinishQuizHandler
{
    // 1. Privata fält för repositories.
    private readonly ISessionRepository _sessionRepository;

    // 2. Konstruktor som tar emot repositories via Dependency Injection.
    public FinishQuizHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    // 3. Handle-metod som innehåller logiken.
    // ÄNDRING: Byter namn från HandleAsync till Handle för att matcha SessionService-anrop.
    public async Task<FinishQuizResult> Handle(FinishQuizCommand cmd, CancellationToken ct)
    {
        // Hämta sessionen från repository.
        var session = await _sessionRepository.GetByIdAsync(cmd.SessionId, ct);

        // Kontrollera att sessionen finns.
        if (session is null)
        {
            throw new ArgumentException($"Session with ID {cmd.SessionId} not found.", nameof(cmd.SessionId));
        }

        // ÄNDRING: Anropa Finish() med DateTime-parameter.
        session.Finish(DateTime.UtcNow);

        // Uppdatera sessionen i repository.
        await _sessionRepository.UpdateAsync(session, ct);

        // ÄNDRING: Returnera resultat med objekt initializer. Score och AnsweredCount konverteras till string.
        return new FinishQuizResult
        {
            Score = session.Score.ToString(),
            AnsweredCount = session.Answers.Count.ToString()
        };
    }
}