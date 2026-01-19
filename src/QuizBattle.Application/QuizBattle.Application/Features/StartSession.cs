
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features;

public class StartSession
{
    public sealed record StartQuizCommand(int questionCount, string? category, int? difficulty);
    
    public sealed record StartQuizResponse(Guid SessionId, IReadOnlyList<Question> Questions);
    
    public sealed class StartQuizHandler
    {
        private readonly IQuestionRepository _questions;
        private readonly ISessionRepository _sessions;

        public StartQuizHandler(
            IQuestionRepository questions,
            ISessionRepository sessions)
        {
            _questions = questions;
            _sessions = sessions;
        }

        public async Task<StartQuizResponse> HandleAsync(
            StartQuizCommand command,
            CancellationToken ct)
        {
            if (command.questionCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(command.questionCount));

            var questions = await _questions.GetRandomAsync(
                command.category,
                command.difficulty,
                command.questionCount,
                ct);

            var session = new QuizSession
            {
                Id = Guid.NewGuid(),
                StartedAtUtc = DateTime.UtcNow,
                QuestionCount = command.questionCount
            };

            await _sessions.SaveAsync(session, ct);

            return new StartQuizResponse(session.Id, questions);
        }
    }


    
}