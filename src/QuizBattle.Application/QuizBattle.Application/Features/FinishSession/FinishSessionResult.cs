using QuizBattle.Application.Features.AnswerQuestion;
using QuizBattle.Application.Features.StartSession;
using QuizBattle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    public sealed record FinishSessionResult(int Score, int AnsweredCount, DateTime FinishedAtUtc);
}
