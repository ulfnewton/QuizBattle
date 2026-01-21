using System;
using System.Threading;
using System.Threading.Tasks;
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public sealed class StartQuizHandler
{
    private readonly IQuestionService _questionService;
    private readonly ISessionRepository _sessionRepository;

    public StartQuizHandler(IQuestionService questionService, ISessionRepository sessionRepository)
    {
        _questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
    }

    public async Task<StartQuizResult> Handle(StartQuizCommand command, CancellationToken ct = default)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));
        if (command.QuestionCount <= 0) throw new ArgumentOutOfRangeException(nameof(command.QuestionCount), "QuestionCount must be > 0.");

        var questions = await _questionService.GetRandomQuestionsAsync(command.QuestionCount, command.Category, command.Difficulty, ct);

        var session = QuizSession.Create(command.QuestionCount);

        await _sessionRepository.SaveAsync(session, ct);

        return new StartQuizResult(session.Id, questions);
    }
}