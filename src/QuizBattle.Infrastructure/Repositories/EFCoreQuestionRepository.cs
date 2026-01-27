using Microsoft.EntityFrameworkCore;
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Infrastructure.Repositories
{
    public class EFCoreQuestionRepository: IQuestionRepository
    {
        private readonly QuizBattleDbContext _context; // används inte och kan tas bort
        private readonly DbSet<Question> _questions;

        public EFCoreQuestionRepository(QuizBattleDbContext context)
        {
            _context = context; // används inte och kan tas bort
            _questions = _context.Questions;
        }

        public async Task<IReadOnlyList<Question>> GetAllAsync(CancellationToken ct = default)
        {
            return await _questions
                .AsNoTracking()     // Ignorera change tracking
                .Include(question => question.Choices)
                .Include(question => question.Answers)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Question>> GetRandomAsync(string? category, int? difficulty, int count, CancellationToken ct)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Count must be greater than zero.");

            IQueryable<Question> query = _questions;

            // Ska vi endast titta på Question i en kategori?
            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(question => IsSameCategory(category!, question));
            }

            // Ska vi endast titta på Question med en specifik svårighetsgrad?
            if (difficulty.HasValue)
            {
                query = query.Where(question => difficulty == question.Difficulty);
            }

            if (count > query.Count())
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, $"Query contains {query.Count()} questions, but expects to contain {count}.");
            }

            // hämta exakt count antal Question
            query = query.OrderBy(question => EF.Functions.Random())
                         .AsNoTracking()
                         .Take(count)
                         .Include(question => question.Choices)
                         .Include(question => question.Answers);

            return await query.ToListAsync(ct);
        }

        public async Task<Question?> GetByCodeAsync(string code, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Code can not be null or contain only whitespace.", nameof(code));
            }

            var query = _questions.Where(question => question.Code == code)
                         .AsNoTracking()
                         .Include(question => question.Choices)
                         .Include(question => question.Answers);

            return await query.FirstOrDefaultAsync(ct);
        }

        private bool IsSameCategory(string category, Question question)
        {
            return category.ToLower().Equals(question.Category?.ToLower());
        }
    }
}
