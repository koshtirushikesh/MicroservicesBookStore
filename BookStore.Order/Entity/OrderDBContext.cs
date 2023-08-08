using Microsoft.EntityFrameworkCore;

namespace BookStore.Order.Entity
{
    public class OrderDBContext : DbContext
    {
        
        public OrderDBContext(DbContextOptions<OrderDBContext> dbContextOptions) : base (dbContextOptions) 
        {
        
        }
        public DbSet<OrderEntity> order { get; set; }
    }
}
