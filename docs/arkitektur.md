# QuizBattle – Namespace, typer och metodsignaturer (v. 001)

Detta dokument beskriver den arkitektoniska strukturen och de tekniska kontrakten för QuizBattle-systemet.

---

## QuizBattle.Domain

Kärnan i systemet som innehåller affärslogik och entiteter (Aggregates).

**Typer/Filer:**

* `Question.cs` → `Question`
* `string Code { get; }`
* `string Text { get; }`
* `List<Choice> Choices { get; }`
* `string CorrectAnswerCode { get; }`
* `string? Category { get; }`
* `int? Difficulty { get; }`
* `bool IsCorrect(string selectedChoiceCode)`
* `Choice GetChoiceAt(int choicePosition)`


* `Choice.cs` → `Choice`
* `Guid Id { get; }`
* `string Code { get; }`
* `string Text { get; }`


* `Answer.cs` → `Answer` (Värdeobjekt/Entitet som lagras i sessionen)
* `Question Question { get; }`
* `string SelectedChoiceCode { get; }`
* `DateTime AnsweredAtUtc { get; }`
* `bool IsCorrect { get; }`


* `QuizSession.cs` → `QuizSession` (Aggregate Root)
* `Guid Id { get; }`
* `DateTime StartedAtUtc { get; }`
* `DateTime? FinishedAtUtc { get; }`
* `int QuestionCount { get; }`
* `IList<Answer> Answers { get; }`
* `int Score { get; }`
* `static QuizSession Create(int count)`
* `void SubmitAnswer(Question question, string selectedChoiceCode, DateTime answeredAtUtc)`
* `void Finish(DateTime finishedAtUtc)`


* `DomainException.cs` → `DomainException`

---

## QuizBattle.Application.Interfaces

Kontrakt (Ports) som definierar hur Application-lagret kommunicerar med omvärlden.

**Typer/Filer:**

* `IQuestionRepository.cs` → `IQuestionRepository`
* `Task<IReadOnlyList<Question>> GetAllAsync(CancellationToken ct)`
* `Task<IReadOnlyList<Question>> GetRandomAsync(string? category, int? difficulty, int count, CancellationToken ct)`
* `Task<Question?> GetByCodeAsync(string code, CancellationToken ct)`


* `ISessionRepository.cs` → `ISessionRepository`
* `Task SaveAsync(QuizSession session, CancellationToken ct)`
* `Task<QuizSession?> GetByIdAsync(Guid id, CancellationToken ct)`
* `Task UpdateAsync(QuizSession session, CancellationToken ct)`


* `IQuestionService.cs` → `IQuestionService` (Höglevlad tjänst för frågor)
* `Task<Question> GetRandomQuestionAsync(string? category, int? difficulty, CancellationToken ct)`


* `ISessionService.cs` → `ISessionService` (Fasad över Use Cases)
* `Task<StartQuizResult> StartAsync(int questionCount, string? cat, int? diff, CancellationToken ct)`
* `Task<AnswerQuestionResult> AnswerAsync(Guid sessionId, string qCode, string choiceCode, CancellationToken ct)`
* `Task<FinishQuizResult> FinishAsync(Guid sessionId, CancellationToken ct)`



---

## QuizBattle.Application.Features (Use Cases)

Innehåller logik för specifika användningsfall genom Commands och Handlers.

**Typer/Filer:**

* `StartQuizHandler.cs` (i `Features.StartSession`)
* `Task<StartQuizResult> Handle(StartQuizCommand cmd, CancellationToken ct)`


* `AnswerQuestionHandler.cs` (i `Features.AnswerQuestion`)
* `Task<AnswerQuestionResult> Handle(AnswerQuestionCommand cmd, CancellationToken ct)`


* `FinishQuizHandler.cs` (i `Features.FinishSession`)
* `Task<FinishQuizResult> Handle(FinishQuizCommand cmd, CancellationToken ct)`



---

## QuizBattle.Infrastructure.Repositories

Implementationer av gränssnitt (Adapters) för datalagring.

**Typer/Filer:**

* `InMemoryQuestionRepository.cs` → `InMemoryQuestionRepository : IQuestionRepository`
* `InMemorySessionRepository.cs` → `InMemorySessionRepository : ISessionRepository`

---

## QuizBattle.Infrastructure.Extensions

**Typer/Filer:**

* `InfrastructureExtensions.cs`
* `static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)`



---

## QuizBattle.Console (Presentation)

Exempel på en klient som konsumerar Application-lagret.

**Typer/Filer:**

* `IConsoleQuestionPresenter.cs` → `IConsoleQuestionPresenter`
* `void DisplayQuestion(Question question, int number)`
* `int PromptForAnswer(Question question)`
