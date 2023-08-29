using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBookServices bookServices;
        private readonly IUserServices userServices;
        private readonly IOrderServices orderServices;
        
        public OrderController(IUserServices userServices, IBookServices bookServices, IOrderServices orderServices)
        {
            this.userServices = userServices;
            this.bookServices = bookServices;
            this.orderServices = orderServices;
            
        }

        [HttpGet("getBookDetails")]
        public async Task<IActionResult> GetBookDetails(int bookID)
        {
            BookEntity book = await bookServices.GetBookDetails(bookID);
            // BookEntity book1 = bookServices.GetBookDetails(bookID);
            if (book != null)
            {
                return Ok(book);
            }
            return BadRequest("unable to get book details");
        }


        [HttpGet("getUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            string token = Request.Headers.Authorization.ToString(); // token will have "Bearer " which we need to remove
            token = token.Substring("Bearer ".Length); // now we will only have the actual jwt token - without Bearer and a space

            UserEntity user = await userServices.GetUserDetails(token);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("unable to get user");
        }

        [Authorize]
        [HttpPost("addOrder")]
        public async Task<IActionResult> AddOrder(int bookID, int quantity)
        {
            string token = Request.Headers.Authorization.ToString(); // token will have "Bearer " which we need to remove
            token = token.Substring("Bearer ".Length);

            OrderEntity orderEntity = await orderServices.AddOrder(bookID, quantity, token);
            if (orderEntity != null)
            {
                return Ok(new ResponseModel<OrderEntity> { Status = true, Message = "succesfully added order", Data = orderEntity });
            }
            return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to place order" });
        }

        [Authorize]
        [HttpGet("getOrders")]
        public async Task<IActionResult> GetOrders()
        {
            string token = Request.Headers.Authorization.ToString(); // token will have "Bearer " which we need to remove
            token = token.Substring("Bearer ".Length);

            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));

            IEnumerable<OrderEntity> orderEntity = await orderServices.GetOrders(userID, token);
            if (orderEntity != null)
            {
                return Ok(new ResponseModel<IEnumerable<OrderEntity>> { Status = true, Message = "succesfully get all orders", Data = orderEntity });
            }
            return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to get the orders" });
        }

        [Authorize]
        [HttpGet("getOrderByOrderID")]
        public async Task<IActionResult> GetOrdersByOrderID(int orderID)
        {
            string token = Request.Headers.Authorization.ToString();
            token = token.Substring("Bearer ".Length);

            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));

            OrderEntity order = await orderServices.GetOrdersByOrderID(orderID, userID, token);
            if (order != null)
            {
                return Ok(new ResponseModel<OrderEntity> { Status = true, Message = "succesfully get order details", Data = order });
            }

            return BadRequest(new ResponseModel<string> { Status = false, Message = "order not found" });
        }

        [Authorize]
        [HttpDelete("removeOrder")]
        public IActionResult RemoveOrder(int orderID)
        {
            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));
            bool isRemove = orderServices.RemoveOrder( orderID, userID);
            if (isRemove)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "succesfully removed order" });
            }
            return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to remove order" });
        }
    }
}
