using Microsoft.EntityFrameworkCore;
using BackEnd.Model;

namespace BackEnd.Data
{

    /// The EF Core database context for the application.
    /// It manages the connection to the database and
    /// provides DbSet properties for querying and saving data.

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// Represents the "Todos" table in the database.
        public DbSet<Todo> Todos { get; set; }
    }
}
