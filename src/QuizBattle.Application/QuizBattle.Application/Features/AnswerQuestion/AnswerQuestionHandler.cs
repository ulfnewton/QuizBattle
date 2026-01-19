using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.AnswerQuestion;

public class AnswerQuestionHandler
{
    // 1. Privata fält för repositories.
    private readonly ISessionRepository _sessionRepository;
    private readonly IQuestionRepository _questionRepository;

    // 2. Konstruktor som tar emot repositories via Dependency Injection.
    public AnswerQuestionHandler(ISessionRepository sessionRepository, IQuestionRepository questionRepository)
    {
        _sessionRepository = sessionRepository;
        _questionRepository = questionRepository;
    }

    // 3. Handle-metod som innehåller logiken.
    public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand cmd, CancellationToken ct)
    {
        // Hämta sessionen från repository.
        var session = await _sessionRepository.GetByIdAsync(cmd.SessionId, ct);

        // Kontrollera att sessionen finns.
        if (session is null)
        {
            throw new ArgumentException($"Session with ID {cmd.SessionId} not found.", nameof(cmd.SessionId));
        }

        // Hämta frågan från repository.
        var question = await _questionRepository.GetByCodeAsync(cmd.QuestionCode, ct);

        // Kontrollera att frågan finns.
        if (question is null)
        {
            throw new ArgumentException($"Question with code {cmd.QuestionCode} not found.", nameof(cmd.QuestionCode));
        }

        // ÄNDRING: Anropa SubmitAnswer med alla 3 parametrar (question, selectedChoiceCode, answeredAtUtc).
        session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);

        // ÄNDRING: IsCorrect beräknas från Question.IsCorrect().
        var isCorrect = question.IsCorrect(cmd.SelectedChoiceCode);

        // Uppdatera sessionen i repository.
        await _sessionRepository.UpdateAsync(session, ct);

        // ÄNDRING: Returnera resultat med primär konstruktor (IsCorrect, CorrectChoiceCode).
        return new AnswerQuestionResult(isCorrect, question.CorrectAnswerCode);
    }
}