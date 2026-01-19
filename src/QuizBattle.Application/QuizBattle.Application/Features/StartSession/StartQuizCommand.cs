using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartSession
{
    /// <summary>
    /// Command = endast indata för att starta en quizsession.
    /// </summary>
    public sealed record StartQuizCommand(
        int QuestionCount,
        string? Category = null,
        int? Difficulty = null
    );
}