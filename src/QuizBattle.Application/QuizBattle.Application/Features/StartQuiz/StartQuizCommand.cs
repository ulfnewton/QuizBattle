using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartQuiz
{
    public sealed record StartQuizCommand(
    int QuestionCount,
    string? Category = null,
    int? Difficulty = null
        );
}
