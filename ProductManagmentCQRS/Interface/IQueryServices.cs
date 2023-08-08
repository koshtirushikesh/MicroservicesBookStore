using ProductManagmentCQRS.QueryModel;

namespace ProductManagmentCQRS.Interface
{
    public interface IQueryServices
    {
        ProductReadModel GetProductById(int ProductID);
        IEnumerable<ProductReadModel> GetAllProducts();
    }
}
