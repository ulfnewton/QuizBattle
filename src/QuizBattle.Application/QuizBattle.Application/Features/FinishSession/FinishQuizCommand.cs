using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    public sealed record FinishQuizCommand(Guid SessionId);
}

/*
 * Hämta en aktiv session
 * avsluta sessionen via session.Finish()
 * spara ändringen
 * returnera total poäng och statistik
 * 
 * FinishQuizCommand
 * FinishQuizHandler
 * FinishQuizResult
 */