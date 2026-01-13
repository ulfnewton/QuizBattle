
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using QuizBattle.Console.Extensions;
using QuizBattle.Domain;

namespace QuizBattle.Console
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _repository = questionRepository;
            EnsureValid();
        }

        public Question GetRandomQuestion()
        {
            var questions = _repository.GetAll();
            return questions[new Random().Next(questions.Count)];
        }

        public List<Question> GetRandomQuestions(int count = 3)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be positive.");
            }

            var questions = _repository.GetAll();

            if (count > questions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be less than or equal the total number of questions.");
            }

            return questions
                .OrderBy(_ => Random.Shared.Next()) // pseudo-slumpordning
                .Take(count)
                .ToList();
        }

        // === Flyttat från QuestionUtils ===

        public void DisplayQuestion(Question question, int number)
        {
            System.Console.WriteLine($"Fråga {number}: {question.Text}");

            for (var i = 0; i < question.Choices.Count; i++)
            {
                var choice = question.Choices[i];
                System.Console.WriteLine($"  {i + 1}. {choice.Text}");
            }
        }

        public int PromptForAnswer(Question question)
        {
            System.Console.Write($"Ditt svar (1–{question.GetChoiceCount()}): ");

            int pick;

            while (!int.TryParse(System.Console.ReadLine(), out pick) || pick < 1 || pick > question.GetChoiceCount())
            {
                System.Console.Write("Ogiltigt val. Försök igen: ");
            }

            return pick;
        }

        private void EnsureValid()
        {
            if (_repository is null)
            {
                throw new DomainException("questionRepository must not be null.");
            }
        }
    }
}
