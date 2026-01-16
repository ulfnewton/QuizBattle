namespace QuizBattle.Application.Features.AnswerQuestion;

/// <summary>
/// Determines the result of answering a question in a quiz session.
/// </summary>
public sealed record AnswerQuestionResult(bool IsCorrect, string CorrectAnswerCode);