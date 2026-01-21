namespace QuizBattle.Application.Features.AnswerQuestion
{
    public sealed record AnswerQuestionCommand(Guid SessionId, string QuestionCode, string SelectedChoiceCode);
}
