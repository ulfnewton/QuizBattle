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
        private readonly QuizBattleDbContext _context;
        private readonly DbSet<Question> _questions;

        public EFCoreQuestionRepository(QuizBattleDbContext context)
        {
            _context = context;
            _questions = _context.Questions;
        }

        public async Task<IReadOnlyList<Question>> GetAllAsync(CancellationToken ct = default)
        {
            return await _questions
                .Include(question => question.Choices)
                .Include(question => question.Answers)
                .AsNoTracking()     // Ignorera change tracking
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
            query = query.OrderBy(question => new Random().NextInt64())
            //EF.Property<int>(new Random().NextInt64(), "random"))
                         .Take(count);
            return await query.ToListAsync(ct);
        }

        public async Task<Question?> GetByCodeAsync(string code, CancellationToken ct)
            => throw new NotImplementedException();

        private bool IsSameCategory(string category, Question question)
        {
            return category.ToLower().Equals(question.Category?.ToLower());
        }
    }
}
