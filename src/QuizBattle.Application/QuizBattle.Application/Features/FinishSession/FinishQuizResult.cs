using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    /// <summary>
    /// Result = utdata efter att ha avslutat en quiz-session.
    /// </summary>
    public sealed record FinishQuizResult(
        Guid SessionId,
        int Score,
        int AnsweredCount,
        int CorrectCount,
        int IncorrectCount,
        DateTime FinishedAtUtc
    );
}