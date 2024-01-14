using Microsoft.EntityFrameworkCore;

namespace Reddot
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ReddotDb");
        }
        public DbSet<Post> Posts { get; set; }
    }
}
