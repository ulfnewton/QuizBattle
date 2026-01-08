namespace QuizBattle.Console;

public class QuizUtils
{
    private static List<Question> _questions = new();
    public static bool IsCompleted()
    {
        throw new NotImplementedException();
    }

    public static void DisplayQuestion()
    {
        throw new NotImplementedException();
    }

    public static int GetAnswer()
    {
        throw new NotImplementedException();
    }

    public static void CheckAnswer(int answer)
    {
        throw new NotImplementedException();
    }

    public static void WriteStatus()
    {
        throw new NotImplementedException();
    }

    public static void SeedQuestions()
    {
        Choice[] choices = {
                new Choice("Q1.1", "Två ben"),
                new Choice("Q1.2", "Tre ben"),
                new Choice("Q1.3","Fyra ben")
        };
        _questions.Add(new Question(choices));
    }
}
