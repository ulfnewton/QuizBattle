using QuizBattle.Application.Features;
using QuizBattle.Application.Interfaces;


namespace QuizBattle.Application.Services
{
    /// <summary>
    /// Fasadar runt use case-handlers för att göra klientanvändning enklare.
    /// Den här klassen innehåller ingen affärslogik – den bara paketerar commands och anropar handlers.
    /// </summary>
    public sealed class SessionService : ISessionService
    {
        private readonly StartSession.StartQuizHandler _start;
        private readonly AnswerQuestion.AnswerQuestionHandler _answer;
        private readonly FinishSession.FinishQuizHandler _finish;

        public SessionService(
            StartSession.StartQuizHandler start,
            AnswerQuestion.AnswerQuestionHandler answer,
            FinishSession.FinishQuizHandler finish)
        {
            _start = start ?? throw new ArgumentNullException(nameof(start));
            _answer = answer ?? throw new ArgumentNullException(nameof(answer));
            _finish = finish ?? throw new ArgumentNullException(nameof(finish));
        }

        public Task<StartSession.StartQuizResponse> StartAsync(
            int questionCount,
            string? category = null,
            int? difficulty = null,
            CancellationToken ct = default)
        {
            var cmd = new StartSession.StartQuizCommand(questionCount, category, difficulty);
            return _start.HandleAsync(cmd, ct);
        }

        public Task<AnswerQuestion.AnswerQuestionResult> AnswerAsync(
            Guid sessionId,
            string questionCode,
            string selectedChoiceCode,
            CancellationToken ct = default)
        {
            var cmd = new AnswerQuestion.AnswerQuestionCommand(sessionId, questionCode, selectedChoiceCode);
            return _answer.Handle(cmd, ct);
        }

        public Task<FinishSession.FinishQuizResult> FinishAsync(Guid sessionId, CancellationToken ct = default)
        {
            var cmd = new FinishSession.FinishQuizCommand(sessionId);
            return _finish.Handle(cmd, ct);
        }
    }
}
