using System;

public sealed class AnswerQuestionCommand
{
    public Guid SessionId { get; }
    public string QuestionCode { get; }
    public string ChoiceCode { get; }

    public AnswerQuestionCommand(Guid sessionId, string questionCode, string choiceCode)
	{
        // User answer a question in an ongoing quiz session
        SessionId = sessionId;
        QuestionCode = questionCode;
        ChoiceCode = choiceCode;
    }
}
