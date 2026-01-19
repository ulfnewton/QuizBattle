using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.FinishQuiz
{
    public sealed record FinishQuizResult(
        int TotalScore,
        IReadOnlyDictionary<string, bool> QuestionStats
    );
}
