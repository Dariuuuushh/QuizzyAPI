using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuizzyAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Dies stellt die "Questions"-Tabelle in der Datenbank dar.
        public DbSet<Question> Questions { get; set; }
    }
}
