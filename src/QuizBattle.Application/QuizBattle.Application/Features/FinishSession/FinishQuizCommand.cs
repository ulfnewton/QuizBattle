namespace QuizBattle.Application.Features.FinishSession;

using System;

public class FinishQuizCommand
{
    public Guid SessionId { get; }

    public FinishQuizCommand(Guid sessionId)
    {
        SessionId = sessionId;
    }
}