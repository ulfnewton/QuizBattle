namespace QuizBattle.Application.Features.AnswerQuestion;

/// <summary>
/// A command used to submit an answer to a specific question in a quiz session.
/// </summary>
public sealed record AnswerQuestionCommand(Guid SessionId, string QuestionCode, string SelectedChoiceCode);