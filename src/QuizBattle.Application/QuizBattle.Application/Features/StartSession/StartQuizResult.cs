using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartSession
{
    public sealed record StartQuizResult
    (
        Guid SessionId,
        IReadOnlyList<Question> Questions
    );
}
