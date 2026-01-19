namespace QuizBattle.Application.Features.AnswerQuestion;

public class AnswerQuestionCommand
{
    public Guid SessionId { get; }
    public string QuestionCode { get; }
    public string SelectedChoiceCode { get; }

    public AnswerQuestionCommand(Guid sessionId, string questionCode, string selectedChoiceCode)
    {
        SessionId = sessionId;
        QuestionCode = questionCode;
        SelectedChoiceCode = selectedChoiceCode;
    }
}