using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public sealed record AnswerQuestionCommand(Guid SessionId, string QuestionCode, string SelectedChoiceCode);
}

/*
 * ANSVAR//RESPONSIBILITY: Hantera inlämning av svar på en fråga i en quiz-session.
 * Ta emot sessionId, questionCode, selectedChoiceCode.
 * Validera att session och fråga finns.
 * Anropa domänlogik: QuizSession.SubmitAnswer(...)
 * Updatera sessionen.
 * Returnera om svaret är rätt eller fel.
 * 
 * SKAPA//CREATE
 * AnswerQuestionCommand
 * AnswerQuestionHandler
 * AnswerQuestionResult
 */