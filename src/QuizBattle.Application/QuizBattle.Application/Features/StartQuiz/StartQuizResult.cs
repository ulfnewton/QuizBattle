using System;

public sealed class StartQuizResult
{
    public Guid QuizSessionId { get; }
    public IReadOnlyList<string> QuestionCodes { get; }

    public StartQuizResult(Guid sessionId, IReadOnlyList<string> questionCodes)
	{
        // Only outdata?
        QuizSessionId = sessionId;
        QuestionCodes = questionCodes;
    }
}
