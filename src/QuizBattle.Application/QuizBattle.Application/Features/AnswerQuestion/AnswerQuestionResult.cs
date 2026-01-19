using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public sealed record AnswerQuestionResult
    (
        Guid SessionId,
        string QuestionCode,
        string SelectedChoiceCode,
        bool IsCorrect,
        int Score,
        int AnsweredCount,
        int QuestionCount,
        bool IsFinished
    );
}
