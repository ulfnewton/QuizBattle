using System;

namespace QuizBattle.Application.Features.StartSession;

public sealed class StartQuizResult
{
    public Guid SessionId { get; }
    public IReadOnlyList<string> QuestionCodes { get; }

    public StartQuizResult(Guid sessionId, IReadOnlyList<string> questionCodes)
	{
        // Only outdata?
        SessionId = sessionId;
        QuestionCodes = questionCodes;
    }
}
