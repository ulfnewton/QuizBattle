using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public sealed class StartQuizHandler
{
   // Repository som ansvarar för all kommunikation med databasen gällande frågor
   private readonly IQuestionRepository _questionRepository;
   
   // Konstruktor som tar emot en instans av IQuestionRepository och ISessionRepository via beroendeinjektion
   private readonly ISessionRepository _sessionRepository;
   
   // Konstruktor till StartQuizHandler som tar emot IQuestionRepository och ISessionRepository
   public StartQuizHandler(IQuestionRepository questionRepository, ISessionRepository sessionRepository)
   {
       _questionRepository = questionRepository;
       _sessionRepository = sessionRepository;
   }

   public async Task<StartQuizResult> Handle(
       StartQuizCommand command,
       CancellationToken ct = default)
   {
       if (command.QuestionCount <= 0)
       {
           throw new ArgumentException(nameof(command.QuestionCount), "QuestionCount måste vara större än noll.");
       }
       
       var questions = await _questionRepository.GetRandomAsync(
           category: command.Category,
           difficulty: command.Difficulty,
           count: command.QuestionCount,
           ct: ct);

       var session = QuizSession.Create(command.QuestionCount);
       
       await _sessionRepository.SaveAsync(session, ct);
       
       return new StartQuizResult(session.Id, questions);
   }
   
   
   
}