using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Application.Features.StartSession
{
    public sealed class StartQuizHandler
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ISessionRepository _sessionRepository;

        public StartQuizHandler(IQuestionRepository questionRepository, ISessionRepository sessionRepository)
        {
            _questionRepository = questionRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<StartQuizResult> Handle(StartQuizCommand cmd, CancellationToken ct = default)
        {
            if (cmd.QuestionCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cmd.QuestionCount), "QuestionCount måste vara större än noll.");
            }

            var questions = await _questionRepository.GetRandomAsync(cmd.Category, cmd.Difficulty, cmd.QuestionCount, ct);
            var session = QuizSession.Create(cmd.QuestionCount);

            await _sessionRepository.SaveAsync(session, ct);

            return new StartQuizResult(session.Id, questions);
        }
    }
}
