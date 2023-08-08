
using Microsoft.EntityFrameworkCore;


namespace BookStore.Users
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> dbContextOptions) : base (dbContextOptions)
        {
            
        }
        public DbSet<UserEntity> User { get; set; }
    }
}
