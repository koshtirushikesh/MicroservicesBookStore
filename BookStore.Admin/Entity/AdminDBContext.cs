using Microsoft.EntityFrameworkCore;

namespace BookStore.Admin.Entity
{
    public class AdminDBContext : DbContext
    {
        public AdminDBContext(DbContextOptions<AdminDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<AdminEntity> Admin { get; set; }
    }
}
