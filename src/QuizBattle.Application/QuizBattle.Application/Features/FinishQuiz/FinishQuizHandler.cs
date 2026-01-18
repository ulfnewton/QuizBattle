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
     public async Task<FinishQuizResult> HandleAsync(FinishQuizCommand command)
     {
        var session = await _sessionRepository.GetByIdAsync(command.SessionId) ?? throw new ArgumentException("Quiz session not found."); // Retrieve session else throw

        // Finish the quiz session
        session.Finish();

        // Save the updated session state
        await _sessionRepository.SaveAsync(session);

        return new FinishQuizResult(session.Score, session.AnsweredQuestionsCount);
     }
}
