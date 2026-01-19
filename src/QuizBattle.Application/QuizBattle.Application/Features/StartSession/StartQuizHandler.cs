using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBattle.Domain;
using QuizBattle.Application.Interfaces;

namespace QuizBattle.Application.Features.StartSession
{
    public sealed class StartQuizHandler
    {
        private readonly IQuestionRepository _questions;
        private readonly ISessionRepository _sessions;

        public StartQuizHandler(IQuestionRepository questions, ISessionRepository sessions)
        {
            _questions = questions ?? throw new ArgumentNullException(nameof(questions));
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
        }

        public async Task<StartQuizResult> Handle(StartQuizCommand command, CancellationToken ct = default)
        {
            if (command is null) throw new ArgumentNullException(nameof(command)); 
            if (command.QuestionCount <= 0)
                throw new ArgumentException("Antal frågor måste vara större än noll.", nameof(command.QuestionCount));

            // Hämta slumpade frågor
            var selectedQuestions = await _questions.GetRandomAsync(
                command.Category,
                command.Difficulty,
                command.QuestionCount,
                ct);


            if (selectedQuestions.Count != command.QuestionCount)
            {
                throw new ArgumentException($"Kunde inte hämta exakt {command.QuestionCount} slumpade frågor.");
            }

            // Skapa en ny quizsession
            var session = QuizSession.Create(command.QuestionCount);

            // Spara session
            await _sessions.SaveAsync(session, ct);

            // Returnera resultatet
            return new StartQuizResult(session.Id, selectedQuestions);
        }
    }
}