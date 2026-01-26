namespace QuizBattle.Application.Features.AnswerQuestion;

/// <summary>
/// Handles the process of answering a question in a quiz session.
/// </summary>
public class AnswerQuestionHandler
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IQuestionRepository _questionRepository;

    public AnswerQuestionHandler(ISessionRepository sessionRepository, IQuestionRepository questionRepository)
    {
        _sessionRepository = sessionRepository;
        _questionRepository = questionRepository;
    }

    /// <summary>
    /// Handles submitting an answer for a specific question in a quiz session and determines
    /// if the answer is correct, returning the appropriate result.
    /// </summary>
    /// <returns>
    /// An object containing the correctness of the user's answer
    /// as well as the actual correct answer code of the question.
    /// </returns>
    public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand cmd, CancellationToken ct)
    {
        var session = await _sessionRepository.GetByIdAsync(cmd.SessionId, ct) 
                      ?? throw new Exception("Session not found");

        var question = await _questionRepository.GetByCodeAsync(cmd.QuestionCode, ct) 
                       ?? throw new Exception("Question not found");

        session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);
        
        await _sessionRepository.UpdateAsync(session, ct);

        return new AnswerQuestionResult(question.IsCorrect(cmd.SelectedChoiceCode), question.CorrectAnswerCode);
    }
}