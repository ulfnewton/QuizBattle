namespace QuizBattle.Domain
{
    public class QuizSession
    {
        // Aggregatets identitet och livscykel
        public Guid Id { get; set; }
        public DateTime StartedAtUtc { get; set; }
        public DateTime? FinishedAtUtc { get; set; }

        // Konfiguration och ackumulerat tillstånd
        public int QuestionCount {  get; set; }
        public IList<Answer> Answers { get; } = new List<Answer>();

        public int Score => Answers.Count(a => a.IsCorrect);

        public static QuizSession Create(int count)
        {
            return new QuizSession
            {
                Id = Guid.NewGuid(),
                StartedAtUtc = DateTime.UtcNow,
                QuestionCount = count,
                // Answers is initialized by the property initializer
            };
        }

        /// <summary>
        /// Registrera ett svar på en fråga inom sessionen.
        /// </summary>
        public void SubmitAnswer(Question question, string selectedChoiceCode, DateTime answeredAtUtc)
        {
            EnsureSessionActive();

            // Tillåt inte dubbla svar på samma fråga (enkel regel)
            if (Answers.Any(a => a.Question.Code.Equals(question.Code, StringComparison.OrdinalIgnoreCase)))
            {
                throw new DomainException($"Frågan '{question.Code}' har redan besvarats i denna session.");
            }

            var answer = new Answer(question, selectedChoiceCode, answeredAtUtc);
            Answers.Add(answer);

            EnsureValid();
        }

        /// <summary>
        /// Avsluta sessionen (idempotent).
        /// </summary>
        public void Finish(DateTime finishedAtUtc)
        {
            if (FinishedAtUtc is not null)
            {
                // Idempotent: tillåt upprepade avslut utan att kasta
                return;
            }

            if (finishedAtUtc == default)
                throw new DomainException("FinishedAtUtc måste vara ett giltigt UTC‑datum.");

            FinishedAtUtc = finishedAtUtc;
            EnsureValid();
        }

        private void EnsureSessionActive()
        {
            EnsureStarted();

            if (FinishedAtUtc is not null)
                throw new DomainException("Sessionen är avslutad. Det går inte att registrera fler svar.");
        }

        private void EnsureStarted()
        {
            if (Id == Guid.Empty)
                throw new DomainException("Id måste vara satt innan sessionen används.");

            if (StartedAtUtc == default)
                throw new DomainException("StartedAtUtc måste vara satt till ett giltigt UTC‑datum.");

            if (QuestionCount <= 0)
                throw new DomainException("QuestionCount måste vara > 0.");
        }

        private void EnsureValid()
        {
            // Basinvarianter
            if (Id == Guid.Empty)
                throw new DomainException("Id får inte vara tomt.");

            if (QuestionCount <= 0)
                throw new DomainException("QuestionCount måste vara > 0.");

            if (StartedAtUtc == default)
                throw new DomainException("StartedAtUtc måste vara satt.");

            // Om avslutad: FinishedAtUtc måste vara giltigt och >= StartedAtUtc
            if (FinishedAtUtc is { } f)
            {
                if (f < StartedAtUtc)
                    throw new DomainException("FinishedAtUtc kan inte vara före StartedAtUtc.");
            }

            // Svar ska avse unika frågor och vara tidsmässigt giltiga
            var duplicateCodes = Answers
                .GroupBy(a => a.Question.Code, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateCodes.Any())
                throw new DomainException($"Dubbla svar på samma fråga: {string.Join(", ", duplicateCodes)}");

            foreach (var a in Answers)
            {
                if (a.AnsweredAtUtc < StartedAtUtc)
                    throw new DomainException($"Svarstid ({a.AnsweredAtUtc:o}) kan inte vara före sessionens start ({StartedAtUtc:o}).");

                if (FinishedAtUtc is DateTime finished && a.AnsweredAtUtc > finished)
                    throw new DomainException($"Svarstid ({a.AnsweredAtUtc:o}) kan inte vara efter sessionens finish ({finished:o}).");
            }

            // Frivillig regel: antalet svar kan inte överstiga QuestionCount
            if (Answers.Count > QuestionCount)
                throw new DomainException($"Antal svar ({Answers.Count}) kan inte överstiga QuestionCount ({QuestionCount}).");
        }
    }
}
