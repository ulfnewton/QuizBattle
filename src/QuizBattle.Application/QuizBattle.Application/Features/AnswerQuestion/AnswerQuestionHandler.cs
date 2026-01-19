using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.AnswerQuestion;

public class AnswerQuestionHandler
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IQuestionRepository _questionRepository;

    public AnswerQuestionHandler(ISessionRepository sessionRepository, IQuestionRepository questionRepository)
    {
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
        _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
    }

    public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand command, CancellationToken ct = default)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));
        if (command.SessionId == Guid.Empty)
            throw new ArgumentException("SessionId får inte vara tomt", nameof(command.SessionId));
        if (string.IsNullOrWhiteSpace(command.QuestionCode))
            throw new ArgumentException("QuestionCode får inte vara tomt", nameof(command.QuestionCode));
        if (string.IsNullOrWhiteSpace(command.SelectedChoiceCode))
            throw new ArgumentException("SelectedChoiceCode får inte vara tomt", nameof(command.SelectedChoiceCode));

        var session = await _sessionRepository.GetByIdAsync(command.SessionId, ct);
        if (session is null)
            throw new ArgumentException($"Session '{command.SessionId}' hittades inte", nameof(command.SessionId));

        var question = await _questionRepository.GetByCodeAsync(command.QuestionCode, ct);
        if (question is null)
            throw new ArgumentException($"Fråga '{command.QuestionCode}' hittades inte", nameof(command.QuestionCode));

        // Validera att valet finns på frågan
        if (!question.HasChoice(command.SelectedChoiceCode))
            throw new ArgumentException(
                $"Val '{command.SelectedChoiceCode}' finns inte för frågan '{question.Code}'.",
                nameof(command.SelectedChoiceCode));

        // Avgör om det är korrekt för resultatet
        var isCorrect = question.IsCorrect(command.SelectedChoiceCode);

        // Låt domain uppdatera sessionen 
        session.SubmitAnswer(question, command.SelectedChoiceCode, DateTime.UtcNow);

        await _sessionRepository.UpdateAsync(session, ct);

        return new AnswerQuestionResult(isCorrect);
    }
}