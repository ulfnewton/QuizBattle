using System;
using System.Threading;
using System.Threading.Tasks;
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.AnswerQuestion;

public sealed class AnswerQuestionHandler
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IQuestionService _questionService;

    public AnswerQuestionHandler(ISessionRepository sessionRepository, IQuestionService questionService)
    {
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
        _questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
    }

    public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand command, CancellationToken ct = default)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));
        if (command.SessionId == Guid.Empty) throw new ArgumentException("SessionId must be set.", nameof(command.SessionId));
        if (string.IsNullOrWhiteSpace(command.QuestionCode)) throw new ArgumentException("QuestionCode must not be empty.", nameof(command.QuestionCode));
        if (string.IsNullOrWhiteSpace(command.SelectedChoiceCode)) throw new ArgumentException("SelectedChoiceCode must not be empty.", nameof(command.SelectedChoiceCode));

        var session = await _sessionRepository.GetByIdAsync(command.SessionId, ct);
        if (session is null) throw new ArgumentException($"Session '{command.SessionId}' not found.", nameof(command.SessionId));

        var question = await _questionService.GetByCodeAsync(command.QuestionCode, ct);
        if (question is null) throw new ArgumentException($"Question '{command.QuestionCode}' not found.", nameof(command.QuestionCode));

        // Delegate business validation to domain
        session.SubmitAnswer(question, command.SelectedChoiceCode, DateTime.UtcNow);

        await _sessionRepository.UpdateAsync(session, ct);

        return new AnswerQuestionResult(question.IsCorrect(command.SelectedChoiceCode), session.Score);
    }
}