using QuizBattle.Console;
using QuizBattle.Console.Extensions;
using QuizBattle.Domain;

var repo = new InMemoryQuestionRepository();
var service = new QuestionService(repo);
var questions = service.GetRandomQuestions();

Console.Title = "QuizBattle – Konsol (v.2 dag 1–2)";
Console.WriteLine("Välkommen till QuizBattle!");
Console.WriteLine("Detta är en minimal code‑along‑loop för dag 1–2 (3 frågor).");
Console.WriteLine("Tryck valfri tangent för att starta...");

Console.ReadKey(true);
Console.WriteLine();

var score = 0;
var asked = 0;

foreach (Question question in questions.Take(3))
{
    asked++;
    QuestionUtils.DisplayQuestion(question, asked);

    int pick = QuestionUtils.PromptForAnswer(question);

    var selected = question.ChoiceAt(pick - 1).Code;
    var correct = question.IsCorrect(selected);

    Console.WriteLine(correct ? "✔ Rätt!" : "✖ Fel.");

    if (correct) score++;

    Console.WriteLine();
}

Console.WriteLine($"Klart! Poäng: {score}/{asked}");
Console.WriteLine("Tryck valfri tangent för att avsluta..."); 
Console.ReadKey(true);
