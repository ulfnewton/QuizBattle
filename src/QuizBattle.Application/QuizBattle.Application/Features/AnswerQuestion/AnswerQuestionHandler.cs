using System;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.AnswerQuestion;
public sealed class AnswerQuestionHandler
{
    // Repository for quiz sessions
    private readonly IQuestionRepository _questionRepository;
    // Repository for questions + added session repository
    private readonly ISessionRepository _sessionRepository;

    public AnswerQuestionHandler(
        ISessionRepository sessionRepository,
        IQuestionRepository questionRepository)
    {
        _sessionRepository = sessionRepository;
        _questionRepository = questionRepository;
    }

    public async Task<AnswerQuestionResult> HandleAsync(AnswerQuestionCommand command, CancellationToken ct)
    {
        // Load session
        var session = await _sessionRepository.GetByIdAsync(command.SessionId)
            ?? throw new ArgumentException("Quiz session not found."); // Session must exist

        // Load question
        var question = await _questionRepository.GetByCodeAsync(
            command.QuestionCode,
            CancellationToken.None)
            ?? throw new ArgumentException("Question not found."); // Question must exist

        // Submit answer (DOMAIN decides correctness)
        session.SubmitAnswer(
            question,
            command.ChoiceCode,
            DateTime.UtcNow);

        // Await saving updated session state
        await _sessionRepository.UpdateAsync(session);

        // Determine correctness from domain state
        var isCorrect = session.Answers
            .Last()
            .IsCorrect;

        return new AnswerQuestionResult(isCorrect);
    }
}
