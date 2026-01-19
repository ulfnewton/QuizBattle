using QuizBattle.Application.Features.AnswerQuestion;
using QuizBattle.Application.Features.FinishSession;
using QuizBattle.Application.Features.StartSession;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Services
{
    /// <summary>
    /// Fasadar runt use case-handlers för att göra klientanvändning enklare.
    /// Den här klassen innehåller ingen affärslogik – den bara paketerar commands och anropar handlers.
    /// </summary>
    public sealed class SessionService : ISessionService
    {
        private readonly StartSessionHandler _start;
        private readonly AnswerQuestionHandler _answer;
        private readonly FinishSessionHandler _finish;

        public SessionService(
            StartSessionHandler start,
            AnswerQuestionHandler answer,
            FinishSessionHandler finish)
        {
            _start = start ?? throw new ArgumentNullException(nameof(start));
            _answer = answer ?? throw new ArgumentNullException(nameof(answer));
            _finish = finish ?? throw new ArgumentNullException(nameof(finish));
        }

        public Task<StartSessionResult> StartAsync(
            int questionCount,
            string? category = null,
            int? difficulty = null,
            CancellationToken ct = default)
        {
            var cmd = new StartSessionCommand(questionCount, category, difficulty);
            return _start.Handle(cmd, ct);
        }

        public Task<AnswerQuestionResult> AnswerAsync(
            Guid sessionId,
            string questionCode,
            string selectedChoiceCode,
            CancellationToken ct = default)
        {
            var cmd = new AnswerQuestionCommand(sessionId, questionCode, selectedChoiceCode);
            return _answer.Handle(cmd, ct);
        }

        public Task<FinishSessionResult> FinishAsync(Guid sessionId, CancellationToken ct = default)
        {
            var cmd = new FinishSessionCommand(sessionId);
            return _finish.Handle(cmd, ct);
        }
    }
}
