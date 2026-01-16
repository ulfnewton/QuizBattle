namespace QuizBattle.Infrastructure.Repositories
{
    /// <summary>
    /// In-memory frågerepository. Används temporärt i API/Console.
    /// </summary>
    public sealed class InMemoryQuestionRepository : IQuestionRepository
    {
        public Task<IReadOnlyList<Question>> GetAllAsync(CancellationToken ct = default)
        {
            return Task.FromResult<IReadOnlyList<Question>>(SeedQuestions().AsReadOnly());
        }

        public Task<Question?> GetByCodeAsync(string code, CancellationToken ct = default)
        {
            var questions = SeedQuestions();
            var question = questions.FirstOrDefault(q =>
                string.Equals(q.Code, code, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(question);
        }

        /// <summary>
        /// Returnerar exakt 'count' slumpade frågor, med frivillig filtrering.
        /// Kastar om det inte går att hämta tillräckligt många.
        /// </summary>
        public Task<IReadOnlyList<Question>> GetRandomAsync(
            string? category,
            int? difficulty,
            int count,
            CancellationToken ct = default)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count måste vara > 0.");

            // Startbas
            IEnumerable<Question> query = SeedQuestions();

            // Filter: kategori
            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(q => string.Equals(q.Category, category, StringComparison.OrdinalIgnoreCase));
            }

            // Filter: svårighet
            if (difficulty is { } d)
            {
                query = query.Where(q => q.Difficulty == d);
            }

            var filtered = query.ToList();

            // Validera att vi har nog många
            if (filtered.Count < count)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(count),
                    $"Begärt antal frågor={count} men det fanns bara {filtered.Count} matchande frågor.");
            }

            // Slumpa ordning och ta count
            var randomized = filtered
                .OrderBy(_ => Random.Shared.Next())
                .Take(count)
                .ToList();

            return Task.FromResult<IReadOnlyList<Question>>(randomized);
        }

        // === Seeddata ===
        private static List<Question> SeedQuestions()
        {
            return new List<Question>
            {
                new Question(
                    code: "Q.CS.001",
                    text: "Vad gör 'using'-statement i C#?",
                    choices: new[]
                    {
                        new Choice("Q.CS.001.A","Skapar en ny tråd"),
                        new Choice("Q.CS.001.B","Säkerställer korrekt Dispose av resurser"),
                        new Choice("Q.CS.001.C","Importerar ett NuGet-paket")
                    },
                    correctAnswerCode: "Q.CS.001.B",
                    category: "CS",
                    difficulty: 2
                ),
                new Question(
                    code: "Q.CS.002",
                    text: "Vad innebär 'var' i C#?",
                    choices: new[]
                    {
                        new Choice("Q.CS.002.A","Dynamisk typ vid runtime"),
                        new Choice("Q.CS.002.B","Implicit, men statisk, typinferens vid compile-time"),
                        new Choice("Q.CS.002.C","Alias för object")
                    },
                    correctAnswerCode: "Q.CS.002.B",
                    category: "CS",
                    difficulty: 2
                ),
                new Question(
                    code: "Q.OOP.011",
                    text: "Vad beskriver inkapsling bäst?",
                    choices: new[]
                    {
                        new Choice("Q.OOP.011.A","Göm implementation, exponera kontrollerat gränssnitt"),
                        new Choice("Q.OOP.011.B","Ärva från flera basklasser"),
                        new Choice("Q.OOP.011.C","Skapa statiska metoder")
                    },
                    correctAnswerCode: "Q.OOP.011.A",
                    category: "OOP",
                    difficulty: 1
                ),
            };
        }
    }
}
