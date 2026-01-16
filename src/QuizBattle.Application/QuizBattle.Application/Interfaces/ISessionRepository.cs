namespace QuizBattle.Application.Interfaces
{
    /// <summary>
    /// Port för att lagra/hämta QuizSession-aggregatet.
    /// Implementationer (in-memory, EF, etc.) finns utanför Application.
    /// </summary>
    public interface ISessionRepository
    {
        Task<QuizSession?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task SaveAsync(QuizSession session, CancellationToken ct = default);
        Task UpdateAsync(QuizSession session, CancellationToken ct = default);
    }
}
