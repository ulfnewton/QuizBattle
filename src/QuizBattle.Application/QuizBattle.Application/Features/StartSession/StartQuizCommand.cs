using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartSession
{
    public class StartQuizCommand
    {
        public StartQuizCommand(int questionCount, string? category, int? difficulty)
        {
            QuestionCount = questionCount;
            Category = category;
            Difficulty = difficulty;
        }
        public int QuestionCount { get; }
        public string? Category { get; }
        public int? Difficulty { get; }
    }
}
