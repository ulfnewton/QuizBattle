using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    /// <summary>
    /// Command = endast indata för att avsluta en quiz-session.
    /// </summary>
    public sealed record FinishQuizCommand(Guid SessionId);
}