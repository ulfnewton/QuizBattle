

namespace QuizBattle.Domain;

public  class Question
{
    public Question(string code, string text, Choice[] choices, string correctAnswerCode)
    {
        Code = code?.Trim()!;
        Text = text?.Trim()!;
        Choices = choices?.ToList()!;
        CorrectAnswerCode = correctAnswerCode;
        EnsureValid();
    }

    public string Code { get; }
    public string Text { get; }
    public List<Choice> Choices { get; }
    public string CorrectAnswerCode { get; }

    public bool IsCorrect(string selectedChoiceCode)
        => string.Equals(selectedChoiceCode, CorrectAnswerCode, StringComparison.OrdinalIgnoreCase);

    private void EnsureValid()
    {
        if (string.IsNullOrWhiteSpace(Text))
        {
            throw new DomainException("Text must not be null or whitespace");
        }

        if (Choices is null)
        {
            throw new DomainException("Choices must not be null");
        }

        if (!Choices.Any())
        {
            throw new DomainException("Choices must not be empty.");
        }

        if (string.IsNullOrWhiteSpace(CorrectAnswerCode))
        {
            throw new DomainException("CorrectAnswerCode must not be null or whitespace.");
        }
    }
}
