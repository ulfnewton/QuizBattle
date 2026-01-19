using System;
using System.Threading;
using System.Threading.Tasks;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.FinishSession
{
   public sealed class FinishSessionHandler
   {
      private readonly ISessionRepository _sessionRepository;
      
      public FinishSessionHandler(ISessionRepository sessionRepository)
      {
         _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
      }

      public async Task<FinishSessionResult> HandleAsync(FinishQuizCommand command, CancellationToken ct = default)
      {
         if (command is null) throw new ArgumentNullException(nameof(command));
         
         var session = await _sessionRepository.GetByIdAsync(command.SessionId, ct);
         if (session == null) throw new InvalidOperationException("Aktiv session hittades inte.");
         
         // Finish the session
         session.Finish(DateTime.UtcNow);
         
         // Persist the change by using UpdateAsync
         await _sessionRepository.UpdateAsync(session, ct);
         
         // Extract total points
         var totalPoints = session.Score;
         var answersCount = session.Answers.Count;

         return new FinishSessionResult(totalPoints, answersCount);
      }
   }
}