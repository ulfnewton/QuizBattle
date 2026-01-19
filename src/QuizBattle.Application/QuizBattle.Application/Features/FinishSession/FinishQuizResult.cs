using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    public sealed record FinishQuizResult(int QuestionCount, int AnsweredCount, int IsCorrectCount, int Score);
}
