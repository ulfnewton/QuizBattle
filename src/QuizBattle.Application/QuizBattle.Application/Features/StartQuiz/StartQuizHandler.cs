using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public sealed class StartQuizHandler
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ISessionRepository _sessionRepository;

    public StartQuizHandler(
        IQuestionRepository questionRepository,
        ISessionRepository sessionRepository)
    {
        _questionRepository = questionRepository;
        _sessionRepository = sessionRepository;
    }

    // Name + signature must match what SessionService calls
    public async Task<StartQuizResult> Handle(StartQuizCommand command, CancellationToken ct)
    {
        if (command.NumberOfQuestions <= 0)
        {
            throw new ArgumentException("Number of questions must be greater than zero.");
        }

        // Get domain questions
        var questions = await _questionRepository.GetRandomAsync(
            category: null,
            difficulty: null,
            count: command.NumberOfQuestions,
            ct: CancellationToken.None);

        // Create session with questions
        var session = QuizSession.Create(command.NumberOfQuestions);

        await _sessionRepository.SaveAsync(session);

        // Return domain questions to Console
        return new StartQuizResult(session.Id, questions);
    }
}
