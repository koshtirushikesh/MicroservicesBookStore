using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface ICartServices
    {
        Task<CartEntity> AddCart(int quantity, int bookID, int userID, string token);
    }
}