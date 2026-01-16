namespace QuizBattle.Domain
{
    public class Question
    {
        public Question(
            string code,
            string text,
            Choice[] choices,
            string correctAnswerCode,
            string? category = null,
            int? difficulty = null)
        {
            Code = code?.Trim()!;
            Text = text?.Trim()!;
            Choices = choices?.ToList()!;
            CorrectAnswerCode = correctAnswerCode?.Trim()!;
            Category = string.IsNullOrWhiteSpace(category) ? null : category.Trim();
            Difficulty = difficulty;
            EnsureValid();
        }

        public string Code { get; }
        public string Text { get; }
        public List<Choice> Choices { get; }
        public string CorrectAnswerCode { get; }
        public string? Category { get; }
        public int? Difficulty { get; }

        public bool IsCorrect(string selectedChoiceCode) =>
            string.Equals(selectedChoiceCode, CorrectAnswerCode, StringComparison.OrdinalIgnoreCase);

        public bool HasChoice(string choiceCode) =>
            Choices.Any(c => string.Equals(c.Code, choiceCode, StringComparison.OrdinalIgnoreCase));

        public Choice GetChoiceOrThrow(string choiceCode)
        {
            var choice = Choices.FirstOrDefault(c =>
                string.Equals(c.Code, choiceCode, StringComparison.OrdinalIgnoreCase));

            return choice ?? throw new DomainException($"Choice '{choiceCode}' finns inte i frågan '{Code}'.");
        }

        private void EnsureValid()
        {
            if (string.IsNullOrWhiteSpace(Code))
                throw new DomainException("Code must not be null or whitespace.");

            if (string.IsNullOrWhiteSpace(Text))
                throw new DomainException("Text must not be null or whitespace.");

            if (Choices is null)
                throw new DomainException("Choices must not be null.");

            if (!Choices.Any())
                throw new DomainException("Choices must not be empty.");

            if (string.IsNullOrWhiteSpace(CorrectAnswerCode))
                throw new DomainException("CorrectAnswerCode must not be null or whitespace.");

            if (!HasChoice(CorrectAnswerCode))
                throw new DomainException($"CorrectAnswerCode '{CorrectAnswerCode}' finns inte bland valen för frågan '{Code}'.");

            if (Difficulty is { } d && (d < 1 || d > 5))
                throw new DomainException("Difficulty (om satt) måste vara mellan 1 och 5.");
        }

        public int GetChoiceCount() => Choices.Count;

        public Choice GetChoiceAt(int choicePosition)
        {
            if (choicePosition  < 0)
                throw new DomainException($"{nameof(Choice)}: {choicePosition}");

            if (choicePosition >= Choices.Count)
                throw new DomainException($"{Choices.Count} choices");

            return Choices[choicePosition];
        }
    }
}
