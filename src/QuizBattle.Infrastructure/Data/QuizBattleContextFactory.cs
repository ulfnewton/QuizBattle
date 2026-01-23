using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace QuizBattle.Infrastructure.Data;

public class QuizBattleContextFactory : IDesignTimeDbContextFactory<QuizBattleDbContext>
{
    public QuizBattleDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<QuizBattleDbContext>();
        optionsBuilder.UseSqlite("Data Source=QuizBattle.db");

        return new QuizBattleDbContext(optionsBuilder.Options);
    }
}
