using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public sealed class StartQuizHandler
{
    // 1. Privata fält för repositories.
    private readonly IQuestionRepository _questionRepository;
    private readonly ISessionRepository _sessionRepository;

    // 2. Konstruktor som tar emot repositories via Dependency Injection.
    public StartQuizHandler(IQuestionRepository questionRepository, ISessionRepository sessionRepository)
    {
        _questionRepository = questionRepository;
        _sessionRepository = sessionRepository;
    }

    // 3. Handle-metod som tar emot kommando och CancellationToken.
    // ÄNDRING: Byter namn från HandleAsync till Handle för att matcha SessionService-anrop.
    public async Task<StartQuizResult> Handle(StartQuizCommand cmd, CancellationToken ct)
    {
        // 4. Validera input.
        if (cmd.QuestionCount <= 0)
        {
            throw new ArgumentException("Question count must be greater than zero.", nameof(cmd.QuestionCount));
        }

        // Hämta slumpade frågor från repository.
        var questions = await _questionRepository.GetRandomAsync(cmd.Category, cmd.Difficulty, cmd.QuestionCount, ct);

        // ÄNDRING: Använd QuizSession.Create() factory-metod istället för konstruktor.
        var session = QuizSession.Create(cmd.QuestionCount);

        // ÄNDRING: Spara sessionen med SaveAsync (inte AddAsync).
        await _sessionRepository.SaveAsync(session, ct);

        // ÄNDRING: Returnera resultat med objekt initializer (inte konstruktor).
        return new StartQuizResult
        {
            SessionId = session.Id,
            Questions = questions
        };
    }
}