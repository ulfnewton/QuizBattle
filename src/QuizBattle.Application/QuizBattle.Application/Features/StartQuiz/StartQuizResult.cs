using System;
using System.Collections.Generic;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public sealed class StartQuizResult
{
    public Guid SessionId { get; }
    public IReadOnlyList<Question> Questions { get; }

    public StartQuizResult(Guid sessionId, IReadOnlyList<Question> questions)
    {
        SessionId = sessionId;
        Questions = questions;
    }
}

