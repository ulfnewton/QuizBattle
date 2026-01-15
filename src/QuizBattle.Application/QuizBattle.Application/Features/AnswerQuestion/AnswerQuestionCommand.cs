using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public class AnswerQuestionCommand
    {
        public AnswerQuestionCommand(
            Guid sessionId,
            string questionCode,
            string selectedChoiceCode)
        {
            SessionId = sessionId;
            QuestionCode = questionCode;
            SelectedChoiceCode = selectedChoiceCode;
        }
        public Guid SessionId { get; }
        public string QuestionCode { get; }
        public string SelectedChoiceCode { get; }
    }
}
