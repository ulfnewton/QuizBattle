using System;
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

public sealed class StartQuizHandler
{
    // Dependencies
    private readonly IQuestionRepository _questionRepository;// Fix IQuestionRespository to the correct one
    private readonly ISessionRepository _sessionRepository;

    // Constructor with dependencies
    public StartQuizHandler(IQuestionRepository questionRepository, ISessionRepository sessionRepository)
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

        // Retrieve random questions for the quiz - adjust method call as needed
        var questions = await _questionRepository
            .GetRandomAsync(category: null,
            difficulty: null,
            count: command.NumberOfQuestions,
            ct: CancellationToken.None);
            
        // Start a new quiz session with the selected questions
        var session = QuizSession.Create(command.NumberOfQuestions); // Renamed to Create for clarity and so it connects to the factory method in QuizSession

        await _sessionRepository.SaveAsync(session);

        // Return the result with session ID and question codes
        return new StartQuizResult(session.Id, questions.Select(q => q.Code).ToList());
    }
}
