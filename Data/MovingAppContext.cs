using Microsoft.EntityFrameworkCore;

using MovingApp.Models;

namespace MovingApp.Data
{
    public class MovingAppContext : DbContext
    {
        public MovingAppContext(
            DbContextOptions<MovingAppContext> options
        ) : base(options)
        {

        }

        public DbSet<MovingTask> Task { get; set; }
    }
}