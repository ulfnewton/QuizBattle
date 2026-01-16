namespace QuizBattle.Application.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IReadOnlyList<Question>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Question>> GetRandomAsync(string? category, int? difficulty, int count, CancellationToken ct);
        Task<Question?> GetByCodeAsync(string code, CancellationToken ct);
    }
}
