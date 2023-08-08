using Microsoft.EntityFrameworkCore;

namespace BookStore.Products.Entity
{
    public class ProductDBContext :  DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> option) : base(option)
        {
            
        }

         public DbSet<BookEntity> Book { get; set; }
    }
}
