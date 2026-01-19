using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    /// <summary>
    /// Result = utdata efter att ha besvarat en fråga i en quiz-session.
    /// </summary>
    public sealed record AnswerQuestionResult(
        bool IsCorrect,
        int Score
    );
}