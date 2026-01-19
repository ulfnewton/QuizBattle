using QuizBattle.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using QuizBattle.Infrastructure.Extensions;
using QuizBattle.Application.Extensions;
using QuizBattle.Console.Presentation;
using QuizBattle.Console.Extensions;

const int numberOfQuestions = 3;

// konfigurera dependency injection (DI) in konsol
var services = new ServiceCollection();

services.AddInfrastructureRepositories()    // Definierad i QuizBattle.Infrastructure
        .AddApplicationServices()
        .AddConsolePresentation();          // Definierad i QuizBattle.Application

// bygg en service provider
var provider = services.BuildServiceProvider();

// skapa ett nytt scope för den här applikationen
using var scope = provider.CreateScope();

// hämta en QuestionService som injicerar IQuestionRepository automatiskt.
var questionService = scope.ServiceProvider.GetRequiredService<IQuestionService>();
var questions = await questionService.GetRandomQuestionsAsync(numberOfQuestions);
var consolePresenter = scope.ServiceProvider.GetRequiredService<IConsoleQuestionPresenter>();

Console.Title = "QuizBattle – Konsol (v.2 dag 1–2)";
Console.WriteLine("Välkommen till QuizBattle!");
Console.WriteLine($"Detta är en minimal code‑along‑loop för dag 1–2 ({numberOfQuestions} frågor).");
Console.WriteLine("Tryck valfri tangent för att starta...");

Console.ReadKey(intercept: true);
Console.WriteLine();

var score = 0;
var asked = 0;

foreach (var question in questions.Take(numberOfQuestions))
{
    asked++;
    consolePresenter.DisplayQuestion(question, asked);

    var pick = consolePresenter.PromptForAnswer(question);

    string selectedCode = question.GetChoiceAt(pick - 1).Code;
    var isCorrect = question.IsCorrect(selectedCode);

    Console.WriteLine(isCorrect ? "✔ Rätt!" : "✖ Fel.");

    if (isCorrect)
    {
        score++;
    }

    Console.WriteLine();
}

Console.WriteLine($"Klart! Poäng: {score}/{asked}");
Console.WriteLine("Tryck valfri tangent för att avsluta...");
Console.ReadKey(intercept: true);
