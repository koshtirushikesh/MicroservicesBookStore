using ProductManagmentCQRS.CommandModel;
using ProductManagmentCQRS.Interface;
using ProductManagmentCQRS.QueryModel;

namespace ProductManagmentCQRS.Services
{
    public class QueryServices : IQueryServices
    {
        private readonly ProductDBContext productDBContext;
        public QueryServices(ProductDBContext productDBContext)
        {
            this.productDBContext = productDBContext;
        }

        IEnumerable<ProductReadModel> IQueryServices.GetAllProducts()
        {
            IEnumerable<ProductReadModel> products = productDBContext.Product.Select(x => new ProductReadModel { ProductID = x.ProductId, Name = x.Name, Description = x.Description, Price = x.Price, Quantity = x.Quantity });

            if (products != null)
            {
                return products;
            }

            return null;
        }

        ProductReadModel IQueryServices.GetProductById(int productID)
        {
            ProductReadModel product = productDBContext.Product.Select(x => new ProductReadModel { ProductID = x.ProductId, Name = x.Name, Description = x.Description, Price = x.Price, Quantity = x.Quantity }).FirstOrDefault(x => x.ProductID == productID);
            if(product != null) 
            {
                return product;
            }
            return null;
        }
    }
}
