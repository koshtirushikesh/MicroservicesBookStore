using BookStore.Order.Entity;
using BookStore.Order.Interface;
using System.Net;

namespace BookStore.Order.Service
{
    public class CartServices : ICartServices
    {
        private readonly OrderDBContext orderDBContext;
        private readonly IUserServices userServices;
        private readonly IBookServices bookServices;
        public CartServices(OrderDBContext orderDBContext, IUserServices userServices, IBookServices bookServices)
        {
            this.orderDBContext = orderDBContext;
            this.userServices = userServices;
            this.bookServices = bookServices;
        }

        public async Task<CartEntity> AddCart(int quantity, int bookID, int userID, string token)
        {
            try
            {
                CartEntity cart = orderDBContext.Cart.FirstOrDefault(x => x.BookID == bookID && x.UserID == userID);
                if (cart == null)
                {
                    CartEntity cart1 = new CartEntity();
                    cart1.UserID = userID;
                    cart1.BookID = bookID;
                    cart1.Quantity = quantity;
                    cart1.User = await userServices.GetUserDetails(token);
                    cart1.book = await bookServices.GetBookDetails(bookID);

                    orderDBContext.Cart.Add(cart1);
                    orderDBContext.SaveChanges();

                    return cart1;
                }
                if (cart != null)
                {
                    cart.Quantity += quantity;
                    cart.User = await userServices.GetUserDetails(token);
                    cart.book = await bookServices.GetBookDetails(bookID);

                    orderDBContext.SaveChanges();
                    return cart;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveCart(int bookID, int userID)
        {
            try
            {
                CartEntity cart = orderDBContext.Cart.FirstOrDefault(x => x.BookID == bookID && x.UserID == userID);
                if (cart != null)
                {
                    orderDBContext.Remove(cart);
                    orderDBContext.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAllCartByUserID(int userID)
        {
            try
            {
                IEnumerable<CartEntity> carts = orderDBContext.Cart.Where(x => x.UserID == userID);
                
                foreach(CartEntity cart in carts)
                {
                    cart.book = await bookServices.GetBookDetails(cart.BookID);
                }
                return carts;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
