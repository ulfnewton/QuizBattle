using System;

namespace QuizBattle.Application.Features.FinishSession;

public sealed class FinishQuizCommand
{
    public Guid SessionId { get; }

    public FinishQuizCommand(Guid sessionId)
	{
        // User finish the quiz and sees the results
        SessionId = sessionId;
    }
}
