using Microsoft.EntityFrameworkCore;
using QuizBattle.Domain;

public sealed partial class QuizBattleDbContext : DbContext
{
    public QuizBattleDbContext(DbContextOptions<QuizBattleDbContext> options) : base(options) { }

    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Choice> Choices => Set<Choice>();
    public DbSet<Answer> Answers => Set<Answer>();
    // Lägg till Sessions om ni vill persistera dem nu

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Question>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany(x => x.Choices)
             .WithOne(x => x.Question)
             .HasForeignKey(x => x.QuestionId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.Answers)
             .WithOne(x => x.Question)
             .HasForeignKey(x => x.QuestionId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        b.Entity<Choice>(e => e.HasKey(x => x.Id));
        b.Entity<Answer>(e => e.HasKey(x => x.Id));
    }
}
