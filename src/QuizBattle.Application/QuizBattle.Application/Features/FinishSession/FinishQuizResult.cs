using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.FinishSession
{
    public class FinishQuizResult
    {
        public FinishQuizResult(int score, int answeredCount, int questionCount)
        {
            Score = score;
            AnsweredCount = answeredCount;
            QuestionCount = questionCount;
        }
        public int Score { get; }
        public int AnsweredCount { get; }
        public int QuestionCount { get; }
    }
}
