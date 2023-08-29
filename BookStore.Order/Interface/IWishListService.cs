using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IWishListService
    {
        Task<WishListEntity> AddWishList(int bookID, int userID,string token);
    }
}