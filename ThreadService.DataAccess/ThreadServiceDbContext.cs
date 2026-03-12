using Microsoft.EntityFrameworkCore;
using ThreadService.Persistance.Entities;

namespace ThreadService.Persistance
{
    public class ThreadServiceDbContext : DbContext
    {
        public ThreadServiceDbContext(DbContextOptions<ThreadServiceDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<ThreadEntity> Threads { get; set; }

    }
}
