using System;

public sealed class StartQuizHandler
{
    // Dependencies
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizSessionRepository _sessionRepository;

    // Constructor with dependencies
    public StartQuizHandler(IQuestionRepository questionRepository, IQuizSessionRepository sessionRepository))
	{
        // Orcest the flow to start a new quiz session?
        _questionRepository = questionRepository;
        _sessionRepository = sessionRepository;
    }

    // Handle the command to start a new quiz
    public StartQuizResult Handle(StartQuizCommand command)
    {
        if (command.NumberOfQuestions <= 0)
        {
           throw new ArgumentException("Number of questions must be greater than zero."); // Validate input
        }

        // Retrieve random questions for the quiz
        var questions = _questionRepository
            .GetRandom(command.NumberOfQuestions)
            .ToList();

        // Start a new quiz session with the selected questions
        var session = QuizSession.StartNew(questions);

        _sessionRepository.Save(session);

        // Return the result with session ID and question codes
        return new StartQuizResult(
            session.Id,
            questions.Select(q => q.Code).ToList()
        );
    }
}
