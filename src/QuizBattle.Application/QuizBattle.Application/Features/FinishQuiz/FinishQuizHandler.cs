using System;

public sealed class FinishQuizHandler
{
    // Dependencies
    private readonly IQuizSessionRepository _sessionRepository;

    public FinishQuizHandler(IQuizSessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository; // Orcest the flow to finish a quiz session?
    }

    // Handle the command to finish the quiz
    public FinishQuizResult Handle(FinishQuizCommand command)
    {
        var session = _sessionRepository.GetById(command.SessionId) ?? throw new ArgumentException("Quiz session not found."); // Retrieve session else throw

        session.Finish(); // Finish the quiz session

        _sessionRepository.Save(session); // Persist changes

        // Return the result
        return new FinishQuizResult(
            session.Score,
            session.AnsweredQuestionsCount
        );
    }
}
