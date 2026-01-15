using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    public class FinishQuizCommand
    {
        public FinishQuizCommand(Guid sessionId)
        {
            SessionId = sessionId;
        }
        public Guid SessionId { get; }
    }
}
