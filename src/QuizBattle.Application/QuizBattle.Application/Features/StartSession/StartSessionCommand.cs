using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartSession
{
    public sealed record StartSessionCommand(int QuestionCount, string? Category = null, int? Difficulty = null);
}
