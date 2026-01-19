using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    public sealed record FinishQuizResult
    (
        Guid SessionId,
        int Score,
        int AnsweredCount,
        int QuestionCount,
        DateTime StartedAtUtc,
        DateTime FinishedAtUtc
    );
}
