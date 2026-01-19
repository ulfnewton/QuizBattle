using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public class AnswerQuestionResult
    {
        public AnswerQuestionResult(bool isCorrect, int answeredCount, int score)
        {
            IsCorrect = isCorrect;
            AnsweredCount = answeredCount;
            Score = score;
        }
        public bool IsCorrect { get; }
        public int AnsweredCount { get; }
        public int Score { get; }
    }
}
