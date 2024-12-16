using Microsoft.EntityFrameworkCore;

namespace QuizzyAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Dies stellt die "Questions"-Tabelle in der Datenbank dar.
        public DbSet<QuestionData> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuestionData>()
                .Property(q => q.Id)
                .ValueGeneratedOnAdd(); 
        }
    }
}
