using static System.Console;
using QuizBattle.Domain;

namespace QuizBattle.Console;

public class QuestionUtils
{
    public static bool IsCompleted()
    {
        throw new NotImplementedException();
    }

    public static void DisplayQuestion()
    {
        System.Console.WriteLine("Hur många ben har en apa? ");
    }

    public static int GetAnswer()
    {
        const int maxCount = 4;

        int pick;

        while (!int.TryParse(System.Console.ReadLine(), out pick) || pick < 1 || pick > maxCount)
        {
            System.Console.Write("Ogiltigt val. Försök igen: ");
        }

        return pick;
    }

    public static void CheckAnswer(int answer)
    {
        // Do nothing
    }

    public static void WriteStatus()
    {
        // Do nothing
    }

    public static List<Question> SeedQuestions()
    {
        List<Question> questions = new();
        
        Choice[] choices = {
                new Choice("Q1.1", "Två ben"),
                new Choice("Q1.2", "Tre ben"),
                new Choice("Q1.3","Fyra ben")
        };
        questions.Add(new Question("Hur många ben har en hund?", choices, "Q1.3"));
        Choice[] choices2 = {
            new Choice("Q2.1", "Fyra ben"),
            new Choice("Q2.2", "Åtta ben"),
            new Choice("Q2.3", "Tio ben"),
            new Choice("Q2.4", "Tolv ben")
        };
        questions.Add(new Question("Hur många ben har en spindel?", choices2, "Q2.2"));

        return questions;
    }
}
