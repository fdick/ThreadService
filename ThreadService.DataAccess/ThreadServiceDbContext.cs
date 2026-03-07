using Microsoft.EntityFrameworkCore;
using ThreadService.DataAccess.Entities;

namespace ThreadService.DataAccess
{
    public class ThreadServiceDbContext : DbContext
    {
        public ThreadServiceDbContext(DbContextOptions<ThreadServiceDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<ThreadEntity> Threads { get; set; }

    }
}
