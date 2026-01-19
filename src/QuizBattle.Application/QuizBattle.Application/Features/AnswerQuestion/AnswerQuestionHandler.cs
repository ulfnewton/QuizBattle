using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.AnswerQuestion;

public sealed class AnswerQuestionHandler
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IQuestionRepository _questionRepository;

    public AnswerQuestionHandler( ISessionRepository sessionRepository, IQuestionRepository questionRepository)
    {
        _sessionRepository = sessionRepository;
        _questionRepository = questionRepository;
    }

    public async Task<AnswerQuestionResult> Handle( AnswerQuestionCommand command, CancellationToken ct)
    {
        var session = await _sessionRepository.GetByIdAsync(command.SessionId) ?? throw new ArgumentException("Quiz session not found."); // Retrieve session else throw

        var question = await _questionRepository.GetByCodeAsync(command.QuestionCode, ct) ?? throw new ArgumentException("Question not found."); // Retrieve question else throw

        session.SubmitAnswer( question, command.ChoiceCode, DateTime.UtcNow);

        await _sessionRepository.UpdateAsync(session);

        var isCorrect = session.Answers.Last().IsCorrect;

        return new AnswerQuestionResult(isCorrect);
    }
}
