using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public class StartQuizHandler
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ISessionRepository _sessionRepository;

    public StartQuizHandler(IQuestionRepository questionRepository, ISessionRepository sessionRepository)
    {
        _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
    }

    public async Task<StartQuizResult> Handle(StartQuizCommand command, CancellationToken ct = default)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));
        if (command.NumberOfQuestions <= 0) 
            throw new ArgumentOutOfRangeException("Antalet frågor måste vara större än 0.", nameof(command.NumberOfQuestions));

        var questions = await _questionRepository.GetRandomAsync(
            category: command.Category,
            difficulty: command.Difficulty,
            count:  command.NumberOfQuestions,
            ct: ct);
        
        var session = QuizSession.Create(command.NumberOfQuestions);
        await _sessionRepository.SaveAsync(session, ct);
        return new StartQuizResult(session.Id, questions);
    }
}