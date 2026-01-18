using System;

public sealed class AnswerQuestionHandler
{
    // Repository for quiz sessions
    private readonly IQuizSessionRepository _sessionRepository;

    public AnswerQuestionHandler(IQuizSessionRepository sessionRepository)
	{
        // Orcest the flow to answer a question in a quiz session?
        _sessionRepository = sessionRepository;
    }

    public async Task <AnswerQuestionResult> HandleAsync(AnswerQuestionCommand command)
    {
        var session = await _sessionRepository.GetByIdAsync(command.SessionId) ?? throw new ArgumentException("Quiz session not found."); // Retrieve session else throw

        // Submit the answer and get correctness
        var isCorrect = session.SubmitAnswer(
            command.QuestionCode,
            command.ChoiceCode
        );

        await _sessionRepository.Save(session);

        // Return the result
        return new AnswerQuestionResult(isCorrect);
    }
}
