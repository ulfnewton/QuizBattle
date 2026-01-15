namespace QuizBattle.Application.Interfaces
{
    public interface IQuestionService
    {
        Task<Question?> GetByCodeAsync(string code, CancellationToken ct = default);
        Task<Question> GetRandomQuestionAsync(string? category = null, int? difficulty = null, CancellationToken ct = default);
        Task<IReadOnlyList<Question>> GetRandomQuestionsAsync(int count, string? category = null, int? difficulty = null, CancellationToken ct = default);
    }
}