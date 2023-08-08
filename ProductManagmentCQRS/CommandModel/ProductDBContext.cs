using Microsoft.EntityFrameworkCore;

namespace ProductManagmentCQRS.CommandModel
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base (options)
        {
            
        }
        public DbSet<ProductAddUpdateModel> Product { get; set; }
    }
}
