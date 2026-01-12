using QuizBattle.Console.Extensions;
using QuizBattle.Domain;
using static System.Console;

namespace QuizBattle.Console;

public class QuestionUtils
{
    public static void DisplayQuestion(Question question, int number)
    {
        System.Console.WriteLine($"Fråga {number}: {question.Text}");

        for (int i = 0; i < question.Choices.Count; i++)
        {
            var choice = question.Choices[i];
            System.Console.WriteLine($"  {i + 1}. {choice.Text}");
        }
    }

    public static List<Question> SeedQuestions()
    {
        return new List<Question>
            {
                new Question(
                    "Q.CS.001", "Vad gör 'using'-statement i C#?",
                    choices: new Choice[]
                    {
                        new Choice("Q.CS.001.A","Skapar en ny tråd"),
                        new Choice("Q.CS.001.B","Säkerställer korrekt Dispose av resurser"),
                        new Choice("Q.CS.001.C","Importerar ett NuGet-paket")
                    },
                    correctAnswerCode: "Q.CS.001.B"),

                new Question(
                    "Q.CS.002", "Vad innebär 'var' i C#?",
                    new[]
                    {
                        new Choice("Q.CS.002.A","Dynamisk typ vid runtime"),
                        new Choice("Q.CS.002.B","Implicit, men statisk, typinferens vid compile-time"),
                        new Choice("Q.CS.002.C","Alias för object")
                    },
                    "Q.CS.002.B"),

                new Question(
                    "Q.OOP.011", "Vad beskriver inkapsling bäst?",
                    new[]
                    {
                        new Choice("Q.OOP.011.A","Göm implementation, exponera kontrollerat gränssnitt"),
                        new Choice("Q.OOP.011.B","Ärva från flera basklasser"),
                        new Choice("Q.OOP.011.C","Skapa statiska metoder")
                    },
                    "Q.OOP.011.A")
            };
    }

    public static int PromptForAnswer(Question question)
    {
        System.Console.Write("Ditt svar (1-" + question.ChoicesCount() + "): ");

        int pick;

        while (!int.TryParse(System.Console.ReadLine(), out pick) || pick < 1 || pick > question.ChoicesCount())
        {
            System.Console.Write("Ogiltigt val. Försök igen: ");
        }

        return pick;
    }
}
