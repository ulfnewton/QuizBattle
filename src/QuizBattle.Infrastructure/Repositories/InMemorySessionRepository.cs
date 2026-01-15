namespace QuizBattle.Infrastructure.Repositories
{
    /// <summary>
    /// Enkel in-memory lagring av QuizSession-aggregat.
    /// </summary>
    public sealed class InMemorySessionRepository : ISessionRepository
    {
        // Vi använder ConcurrentDictionary eftersom det inte är känsligt för race conditions
        private static readonly ConcurrentDictionary<Guid, QuizSession> _store = new();

        // Hämta en quiz sessions med hjälp av Id
        public Task<QuizSession?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            _store.TryGetValue(id, out var session);
            return Task.FromResult(session);
        }

        // Spara quiz sessionen 
        public Task SaveAsync(QuizSession session, CancellationToken ct = default)
        {
            if (session is null) throw new ArgumentNullException(nameof(session));

            // Om sessionen finns redan: kasta
            // Alternativt skulle vi kunna uppdatera – här väljer vi att skydda mot dubletter.
            // En vinst med detta är att vi lättare hittar logikfel i koden.
            if (!_store.TryAdd(session.Id, session))
            {
                throw new DomainException($"Session med Id '{session.Id}' finns redan.");
            }

            return Task.CompletedTask;
        }

        // Uppdatera quiz session
        public Task UpdateAsync(QuizSession session, CancellationToken ct = default)
        {
            if (session is null) throw new ArgumentNullException(nameof(session));

            // Uppdatera in-place (ConcurrentDictionary lagrar referensen; i in-memory räcker detta)
            _store[session.Id] = session;
            return Task.CompletedTask;
        }
    }
}
