using Microsoft.Extensions.DependencyInjection;
using QuizBattle.Application.Extensions;
using QuizBattle.Application.Interfaces;
using QuizBattle.Application.Services;
using QuizBattle.Console.Extensions;
using QuizBattle.Console.Presentation;
using QuizBattle.Infrastructure.Extensions;

const int numberOfQuestions = 3;

// konfigurera dependency injection (DI) in konsol
var services = new ServiceCollection();

services.AddInfrastructureRepositories()    // Definierad i QuizBattle.Infrastructure
        .AddApplicationServices()           // Definierad i QuizBattle.Application
        .AddConsolePresentation();          // Definierad i QuizBattle.Console

// bygg en service provider
var provider = services.BuildServiceProvider();

// skapa ett nytt scope för den här applikationen
using var scope = provider.CreateScope();

// hämtar QuestionService och SessionService med korrekt beroenden (repository)
var questionService = scope.ServiceProvider.GetRequiredService<IQuestionService>();
var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
var presenter = scope.ServiceProvider.GetRequiredService<IConsoleQuestionPresenter>();

Console.Title = "QuizBattle – Konsol (v.2 dag 1–2)";
Console.WriteLine("Välkommen till QuizBattle!");
Console.WriteLine($"Detta är en minimal code‑along‑loop för dag 1–2 ({numberOfQuestions} frågor).");
Console.WriteLine("Tryck valfri tangent för att starta...");

Console.ReadKey(intercept: true);
Console.WriteLine();

// Hämta slumpvis antal frågor
var questions = await questionService.GetRandomQuestionsAsync(numberOfQuestions);

// Starta en session via Application
var start = await sessionService.StartAsync(questionCount: 3);

// UI-loop (Console-only)
var score = 0;
var asked = 0;

foreach (var question in start.Questions)
{
    asked++;
    presenter.DisplayQuestion(question, asked);

    var pick = presenter.PromptForAnswer(question);
    var selectedCode = question.GetChoiceAt(pick - 1).Code;

    // Registrera svar i applikationen (handlers via SessionService)
    var answerResult = await sessionService.AnswerAsync(start.SessionId, question.Code, selectedCode);

    System.Console.WriteLine(answerResult.IsCorrect ? "✔ Rätt!" : "✖ Fel.");
    if (answerResult.IsCorrect) score++;
    System.Console.WriteLine();
}

var finished = await sessionService.FinishAsync(start.SessionId);
System.Console.WriteLine($"Klart! Poäng: {finished.Score}/{finished.AnsweredCount}");
System.Console.WriteLine("Tryck valfri tangent för att avsluta...");
System.Console.ReadKey(intercept: true);
