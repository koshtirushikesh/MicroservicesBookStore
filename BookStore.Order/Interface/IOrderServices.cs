using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IOrderServices
    {
        Task<OrderEntity> AddOrder(int bookID, int quantity, string token);
        
        Task<IEnumerable<OrderEntity>> GetOrders(int userID, string token);

        Task<OrderEntity> GetOrdersByOrderID(int orderID, int userID, string token);

        bool RemoveOrder(int orderID, int userID);
    }
}
