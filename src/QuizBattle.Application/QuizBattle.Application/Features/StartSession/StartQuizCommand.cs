using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartSession
{
    public sealed record StartQuizCommand(int QuestionCount, string? Category = null, int? Difficulty = null);
}


/*
 * Ta emot antal frågor
 * hämta slumpmässiga frågor via IQuestionService
 * skapa och spara ny session
 * returnera sessionId och valda frågor
 * 
 * StartQuizCommand
 * StartQuizHandler
 * StartQuizResult
 */