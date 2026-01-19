using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public sealed class AnswerQuestionHandler
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ISessionRepository _sessionRepository;

        public AnswerQuestionHandler(IQuestionRepository questionRepository, ISessionRepository sessionRepository)
        {
            _questionRepository = questionRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand cmd, CancellationToken ct = default)
        {
            var session = await _sessionRepository.GetByIdAsync(cmd.SessionId, ct);
            if (session == null)
                throw new ArgumentException("Session not found.");
            var question = await _questionRepository.GetByCodeAsync(cmd.QuestionCode, ct);
            if (question == null)
                throw new ArgumentException("Question not found.");
            session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);

            await _sessionRepository.UpdateAsync(session, ct);

            return new AnswerQuestionResult(question.IsCorrect(cmd.SelectedChoiceCode));
        }
    }
}
