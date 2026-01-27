using Microsoft.EntityFrameworkCore;
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Infrastructure.Repositories
{
    public class EFCoreSessionRepository : ISessionRepository
    {
        private readonly QuizBattleDbContext _context;
        private readonly DbSet<QuizSession> _sessions;

        public EFCoreSessionRepository(QuizBattleDbContext context) 
        {
            _context = context;
            _sessions = context.Sessions;
        }

        public async Task<QuizSession?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            //var session = await _sessions.FindAsync(new[] { id }, ct);
            //return session;

            return await _sessions.Where(session => session.Id == id)
                                  .AsNoTracking()
                                  .Include(session => session.Answers)
                                  .ThenInclude(answer => answer.Question)
                                  .FirstOrDefaultAsync(ct);
        }

        public async Task SaveAsync(QuizSession session, CancellationToken ct = default)
        {
            if (session is null)
                throw new ArgumentNullException(nameof(session));

            // finns session i databasen?
            var existingSession = await GetByIdAsync(session.Id, ct);
            
            if (existingSession is not null)
            {
                throw new DomainException($"Session med Id '{session.Id}' finns redan.");
            }

            await _sessions.AddAsync(session, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(QuizSession session, CancellationToken ct = default)
        {
            if (session is null) throw new ArgumentNullException(nameof(session));

            _sessions.Update(session);
            await _context.SaveChangesAsync(ct);
        }
    }
}
