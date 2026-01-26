namespace QuizBattle.Application.Features.StartSession;

/// <summary>
/// Handles the process of starting a quiz session by generating questions and creating a session.
/// </summary>
public sealed class StartSessionHandler
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IQuestionRepository _questionRepository;

    /// <summary>
    /// A command handler responsible for starting a quiz session.
    /// It generates a set of quiz questions based on the specified command parameters
    /// and creates a new quiz session, saving it to the repository.
    /// </summary>
    public StartSessionHandler(ISessionRepository sessionRepository, IQuestionRepository questionRepository)
    {
        _sessionRepository = sessionRepository;
        _questionRepository = questionRepository;
    }

    /// <summary>
    /// Handles the command for starting a quiz session. Generates quiz questions
    /// based on the specified parameters and creates a corresponding session, then saving it to the repository.
    /// </summary>
    /// <returns> Returns the ID of the created session and a generated list of quiz questions.</returns>
    public async Task<StartSessionResult> Handle(StartSessionCommand cmd, CancellationToken ct)
    {
        if (cmd.QuestionCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(cmd.QuestionCount));
        
        var questions = await _questionRepository.GetRandomAsync(cmd.Category, cmd.Difficulty, cmd.QuestionCount, ct);
        
        var session = new QuizSession
        {
            Id = Guid.NewGuid(),
            StartedAtUtc = DateTime.UtcNow,
            QuestionCount = cmd.QuestionCount
        };
        
        await _sessionRepository.SaveAsync(session, ct);

        return new StartSessionResult(session.Id, questions);
    }
}