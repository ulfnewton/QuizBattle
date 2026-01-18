using System;
using QuizBattle.Domain;

public sealed class StartQuizHandler
{
    // Dependencies
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizSessionRepository _sessionRepository;

    // Constructor with dependencies
    public StartQuizHandler(IQuestionRepository questionRepository, IQuizSessionRepository sessionRepository)
	{
        // Orcest the flow to start a new quiz session?
        _questionRepository = questionRepository;
        _sessionRepository = sessionRepository;
    }

    // Handle the command to start a new quiz
    public async Task<StartQuizResult> HandleAsync(StartQuizCommand command) //Change to async
    {
        if (command.NumberOfQuestions <= 0)
        {
           throw new ArgumentException("Number of questions must be greater than zero."); // Validate input
        }

        // Retrieve random questions for the quiz
        var questions = _questionRepository
            .GetRandomAsync(command.NumberOfQuestions);
            
        // Start a new quiz session with the selected questions
        var session = QuizSession.Create(questions); // Renamed to Create for clarity and so it connects to the factory method in QuizSession

        await _sessionRepository.SaveAsync(session);

        // Return the result with session ID and question codes
        return new StartQuizResult(session.Id, questions.Select(q => q.Code).ToList());
    }
}
