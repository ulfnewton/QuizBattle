using QuizBattle.Application.Features.AnswerQuestion;
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.FinishSession
{
    public class FinishQuizHandler
    {
        private readonly ISessionRepository _sessions;

        public FinishQuizHandler(ISessionRepository sessions)
        {
            _sessions = sessions;
        }

        public async Task<FinishQuizResult> HandleAsync(FinishQuizCommand cmd, CancellationToken ct = default)
        {
            try
            {
                var session = await _sessions.GetByIdAsync(cmd.SessionId, ct);

                if (session is null)
                {
                    return FinishQuizResult.Fail(cmd.SessionId, $"Session med Id '{cmd.SessionId}' existerar inte.");
                }

                session.Finish(DateTime.UtcNow);

                // anropar nu SaveAsync
                await _sessions.SaveAsync(session, ct);

                return new FinishQuizResult(
                    true,
                    session.Id,
                    session.Answers.Count,
                    session.Score,
                    session.StartedAtUtc,
                    session.FinishedAtUtc!.Value
                );
            }
            catch (DomainException ex)
            {
                // Domänfel (t.ex. session redan avslutad, fråga redan besvarad)
                return FinishQuizResult.Fail(cmd.SessionId, ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Valideringsfel från domänlogiken
                return FinishQuizResult.Fail(cmd.SessionId, ex.Message);
            }
        }
    }
}
