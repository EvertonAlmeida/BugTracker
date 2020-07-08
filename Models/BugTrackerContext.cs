using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    public class BugTrackerContext : DbContext
    {
        public BugTrackerContext(DbContextOptions<BugTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Status> Status { get; set; }
    }
}