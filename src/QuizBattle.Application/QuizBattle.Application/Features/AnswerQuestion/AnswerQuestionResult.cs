using System;

namespace QuizBattle.Application.Features.AnswerQuestion;

public sealed record AnswerQuestionResult(bool IsCorrect, string CorrectAnswerCode);
