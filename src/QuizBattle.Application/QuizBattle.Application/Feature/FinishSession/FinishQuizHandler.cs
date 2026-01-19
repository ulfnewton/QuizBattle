using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.FinishSession
{
    public class FinishQuizHandler
    {
        private readonly ISessionRepository _sessions;

        public FinishQuizHandler(ISessionRepository sessions)
        {
            _sessions = sessions;
        }

        public async Task<FinishQuizResult> Handle(FinishQuizCommand cmd)
        {
            try
            {
                var session = await _sessions.GetByIdAsync(cmd.SessionId, default);

                // Avsluta sessionen med nuvarande tid
                session.Finish(DateTime.UtcNow);

                await _sessions.UpdateAsync(session, default);

                // Mappar resultatet manuellt här
                return new FinishQuizResult(
                    true,
                    session.Id,
                    session.Answers.Count,
                    session.Score,
                    session.StartedAtUtc,
                    session.FinishedAtUtc.Value // Den bör ha ett värde nu efter Finish()
                );
            }
            catch (Exception ex)
            {
                return FinishQuizResult.Fail(cmd.SessionId, ex.Message);
            }
        }
    }
}
