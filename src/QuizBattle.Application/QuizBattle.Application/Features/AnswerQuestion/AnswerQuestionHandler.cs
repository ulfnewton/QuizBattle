using QuizBattle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public class AnswerQuestionHandler
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IQuestionRepository _questionRepository;

        public AnswerQuestionHandler(ISessionRepository sessionRepository, IQuestionRepository questionRepository)
        {
            _sessionRepository = sessionRepository;
            _questionRepository = questionRepository;
        }

        public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand cmd, CancellationToken ct = default)
        {
            var session = await _sessionRepository.GetByIdAsync(cmd.SessionId, ct);
            if (cmd.SessionId == null)
            {
                throw new ArgumentException("Session id not found");
            }

            var question = await _questionRepository.GetByCodeAsync(cmd.QuestionCode, ct);
            if (cmd.QuestionCode == null)
            {
                throw new ArgumentException("Question not found");
            }

            session.SubmitAnswer(question, cmd.SelectedChoiceCode, DateTime.UtcNow);
            await _sessionRepository.UpdateAsync(session, ct);

            return new AnswerQuestionResult(question.IsCorrect(cmd.SelectedChoiceCode), question.CorrectAnswerCode);
        }

        
    }
}
