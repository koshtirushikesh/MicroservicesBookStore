using ProductManagmentCQRS.CommandModel;

namespace ProductManagmentCQRS.Interface
{
    public interface ICommandServices
    {
        ProductAddUpdateModel AddProduct(ProductAddUpdateModel product);
        ProductAddUpdateModel UpdateProduct(ProductAddUpdateModel product, int id);
        bool DeleteProduct(int id);
    }
}
