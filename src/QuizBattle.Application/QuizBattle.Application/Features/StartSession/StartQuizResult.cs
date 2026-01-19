using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession
{
    /// <summary>
    /// Result = det som returneras när en quizsession har startats.
    /// </summary>
    public sealed record StartQuizResult(
        Guid SessionId,
        IReadOnlyList<Question> Questions
    );
}