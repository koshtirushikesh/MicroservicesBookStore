using BookStore.Order.Entity;
using BookStore.Order.Migrations;

namespace BookStore.Order.Interface
{
    public interface IWishListService
    {
        Task<WishListEntity> AddWishList(int bookID, int userID,string token);
        bool RemoveWishList(int bookID, int userID);
        Task<IEnumerable<WishListEntity>> GetWishListByUserID(int userID);
    }
}