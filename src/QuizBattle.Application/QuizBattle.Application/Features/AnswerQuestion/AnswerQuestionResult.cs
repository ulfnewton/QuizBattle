using QuizBattle.Application.Features.FinishSession;
using QuizBattle.Application.Features.StartSession;
using QuizBattle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public sealed record AnswerQuestionResult(bool IsCorrect, string CorrectAnswerCode);
}
