using System;
using QuizBattle.Application.Interfaces;

public sealed class FinishQuizHandler
{
    // Dependencies
    private readonly ISessionRepository _sessionRepository;

    public FinishQuizHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository; // Orcest the flow to finish a quiz session?
    }

    // Handle the command to finish the quiz
     public async Task<FinishQuizResult> HandleAsync(FinishQuizCommand command)
     {
        var session = await _sessionRepository.GetByIdAsync(command.SessionId) ?? throw new ArgumentException("Quiz session not found."); // Retrieve session else throw

        // Finish the quiz session
        session.Finish(DateTime.UtcNow); // Added UtcNow to indicate time of finishing

        // Save the updated session state
        await _sessionRepository.UpdateAsync(session);

        return new FinishQuizResult(session.Score, session.Answers.Count);
     }
}
