using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession
{
    public sealed class StartSessionHandler(
        IQuestionRepository questionRepository,
        ISessionRepository sessionRepository)
    {
        public async Task<StartSessionResponse> HandleAsync(StartSessionCommand cmd, CancellationToken ct)
        {
            // Validation on application-level
            if (cmd.QuestionCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(cmd.QuestionCount));
            
            // Get questions via repository
            var qs = await questionRepository.GetRandomAsync(cmd.Category, cmd.Difficulty, cmd.QuestionCount, ct);
            
            // Create session
            var session = new QuizSession
            {
                Id = Guid.NewGuid(),
                StartedAtUtc = DateTime.UtcNow,
                QuestionCount = cmd.QuestionCount
            };
            
            // Save session
            await sessionRepository.SaveAsync(session, ct);
            
            // Return response 
            return new StartSessionResponse(session.Id, qs);
        }
    }
}