using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    /// <summary>
    /// Command = endast indata för att besvara en fråga i en quiz-session.
    /// </summary>
    public sealed record AnswerQuestionCommand(
        Guid SessionId,
        string QuestionCode,
        string SelectedChoiceCode
    );
}