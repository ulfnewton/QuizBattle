using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.AnswerQuestion;

public sealed class AnswerQuestionHandler
{
    // Repositories
    private readonly ISessionRepository _sessionRepository;
    
    // Repositories
    private readonly IQuestionRepository _questionRepository;

    // Constructor
    public AnswerQuestionHandler(ISessionRepository sessionRepository, IQuestionRepository questionRepository)
    {
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
        _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
    }

    // Handle Method
    public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand command, CancellationToken ct = default)
    {
        // Hämta sessionen från repositoryt
        var session = await _sessionRepository.GetByIdAsync(command.SessionId, ct)
            ?? throw new InvalidOperationException("session not found");
        
        // Hämta frågan från repositoryt
        var question = await _questionRepository.GetByCodeAsync(command.QuestionCode, ct)
            ?? throw new InvalidOperationException("question not found");
        
        // Skicka in svaret i sessionen
        session.SubmitAnswer(question, command.SelectedChoiceCode, DateTime.UtcNow);
        
        // Uppdatera sessionen i repositoryt
        await _sessionRepository.UpdateAsync(session, ct);
        
        // Hämta det senaste svaret från sessionen
        var lastAnswer = session.Answers.Last();

        // Returnera resultatet av svaret
        return new AnswerQuestionResult(lastAnswer.IsCorrect, session.Score);
    }
}