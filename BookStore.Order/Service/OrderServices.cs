using BookStore.Order.Entity;
using BookStore.Order.Interface;

namespace BookStore.Order.Service
{

    public class OrderServices : IOrderServices
    {
        // add order -> return OrderEntity -> User and BookDetails and order amount -> parameters -> bookid, quantity, token
        private readonly OrderDBContext orderDBContext;
        private readonly IBookServices bookServices;
        private readonly IUserServices userServices;
        public OrderServices(OrderDBContext orderDBContext, IBookServices bookServices, IUserServices userServices)
        {
            this.orderDBContext = orderDBContext;
            this.bookServices = bookServices;
            this.userServices = userServices;
        }

        public async Task<OrderEntity> AddOrder(int bookID, int quantity, string token)
        {

            BookEntity book = await bookServices.GetBookDetails(bookID);
            UserEntity user = await userServices.GetUserDetails(token);

            OrderEntity orderEntity = new OrderEntity();
            orderEntity.UserID = user.UserID;
            orderEntity.Quantity = quantity;
            orderEntity.BookID = bookID;

            orderEntity.Book = book;
            orderEntity.User = user;

            orderEntity.OrderAmout = book.actualprice - book.discountedprice;

            if (user.UserID != null && bookID != null && quantity != 0)
            {
                orderDBContext.order.Add(orderEntity);
                orderDBContext.SaveChanges();
                return orderEntity;
            }
            return null;

        }


        // view order -> return OrderEntity with order amount and user, book details
        // delete order

        public async Task<IEnumerable<OrderEntity>> GetOrders(int userID, string token)
        {
            IEnumerable<OrderEntity> result = orderDBContext.order.Where(x => x.UserID == userID);

            if (result != null)
            {
                foreach (OrderEntity order in result)
                {
                    order.Book = await bookServices.GetBookDetails(order.BookID);
                    order.User = await userServices.GetUserDetails(token);
                }
                return result;
            }
            return null;
        }

        public async Task<OrderEntity> GetOrdersByOrderID(int orderID, int userID, string token)
        {
            OrderEntity orderEntity = orderDBContext.order.Where(x => x.OrderID == orderID && x.UserID == userID).FirstOrDefault();
            if (orderEntity != null)
            {
                orderEntity.Book = await bookServices.GetBookDetails(orderEntity.BookID);
                orderEntity.User = await userServices.GetUserDetails(token);

                return orderEntity;
            }
            return null;
        }

        public bool RemoveOrder(int orderID, int userID)
        {
            OrderEntity orderEntity = orderDBContext.order.Where(x => x.OrderID == orderID && x.UserID == userID).FirstOrDefault();
            if (orderEntity != null)
            {
                orderDBContext.order.Remove(orderEntity);
                orderDBContext.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
