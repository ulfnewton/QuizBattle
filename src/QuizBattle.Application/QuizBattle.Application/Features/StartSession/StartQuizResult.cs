using QuizBattle.Application.Services;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartSession
{
    public class StartQuizResult
    {
        public StartQuizResult(Guid sessionId, IReadOnlyList<Question> questions)
        {
            SessionId = sessionId;
            Questions = questions;
        }
        public Guid SessionId { get; }
        public IReadOnlyList<Question> Questions { get; }
    }
}
