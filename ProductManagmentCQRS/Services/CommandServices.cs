using ProductManagmentCQRS.CommandModel;
using ProductManagmentCQRS.Interface;

namespace ProductManagmentCQRS.Services
{
    public class CommandServices : ICommandServices
    {
        private readonly ProductDBContext productDBContext;
        public CommandServices(ProductDBContext productDBContext)
        {
            this.productDBContext = productDBContext;
        }

        public ProductAddUpdateModel AddProduct(ProductAddUpdateModel product)
        {
            bool isProduct = productDBContext.Product.Any(x => x.Name == product.Name);
            if (!isProduct)
            {
                productDBContext.Product.Add(product);
                productDBContext.SaveChanges();

                return product;
            }
            return null;
        }

        public bool DeleteProduct(int id)
        {
            ProductAddUpdateModel product = productDBContext.Product.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                productDBContext.Product.Remove(product);
                productDBContext.SaveChanges();
                return true;
            }

            return false;
        }

        public ProductAddUpdateModel UpdateProduct(ProductAddUpdateModel updateProduct,int productID)
        {
            ProductAddUpdateModel product = productDBContext.Product.FirstOrDefault(x => x.ProductId == productID);
            if(product != null)
            {
                product.Name= updateProduct.Name;
                product.Description = updateProduct.Description;
                product.Quantity = updateProduct.Quantity;
                product.Price = updateProduct.Price;
                product.UpdatedAt = DateTime.Now;

                
                productDBContext.SaveChanges();
                return product;
            }
            return null;
        }
    }
}
