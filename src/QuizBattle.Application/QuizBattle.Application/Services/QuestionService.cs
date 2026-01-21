using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Services
{
    /// <summary>
    /// Tjänst för att hämta frågor via IQuestionRepository.
    /// Innehåller ingen UI (ingen Console, inga statuskoder).
    /// </summary>
    public sealed class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _repository = questionRepository;
            EnsureValid();
        }

        public async Task<Question> GetRandomQuestionAsync(
            string? category = null,
            int? difficulty = null,
            CancellationToken ct = default)
        {
            var questions = await _repository.GetRandomAsync(category, difficulty, 1, ct);
            if (questions is null || questions.Count == 0)
                throw new DomainException("Kunde inte hämta en slumpad fråga.");

            return questions[0];
        }

        public async Task<IReadOnlyList<Question>> GetRandomQuestionsAsync(
            int count,
            string? category = null,
            int? difficulty = null,
            CancellationToken ct = default)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count måste vara > 0.");

            var questions = await _repository.GetRandomAsync(category, difficulty, count, ct);
            if (questions is null || questions.Count != count)
                throw new DomainException($"Kunde inte hämta exakt {count} slumpade frågor.");

            return questions;
        }

        public Task<Question?> GetByCodeAsync(string code, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code får inte vara tomt.", nameof(code));

            return _repository.GetByCodeAsync(code, ct);
        }

        public async Task<Question> GetRandomQuestionAsync(CancellationToken ct = default)
        {
            var questions = await _repository.GetRandomAsync(
                category: null,
                difficulty: null,
                1,
                ct);
            return questions[new Random().Next(questions.Count)];
        }

        public async Task<List<Question>> GetRandomQuestionsAsync(int count = 3, CancellationToken ct = default)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be positive.");
            }

            var questions = await _repository.GetAllAsync();

            if (count > questions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be less than or equal the total number of questions.");
            }

            return questions
                .OrderBy(_ => Random.Shared.Next()) // pseudo-slumpordning
                .Take(count)
                .ToList();
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
