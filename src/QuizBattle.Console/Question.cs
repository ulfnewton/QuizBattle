namespace QuizBattle.Console;

public  class Question
{
    public Question(Choice[] choices, string correctAnswerCode)
    {
        Choices = choices.ToList();
        CorrectAnswerCode = correctAnswerCode;
    }

    public List<Choice> Choices { get; }
    public string CorrectAnswerCode { get; }
}