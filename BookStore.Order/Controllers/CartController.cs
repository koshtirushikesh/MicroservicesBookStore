using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices cartServices;
        public CartController(ICartServices cartServices)
        {
            this.cartServices = cartServices;
        }

        [Authorize]
        [HttpPost("addCart")]
        public async Task<IActionResult> AddCart(int bookID,int quantity)
        {
            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));

            string token = Request.Headers.Authorization.ToString(); // token will have "Bearer " which we need to remove
            token = token.Substring("Bearer ".Length); // now we will only have the actual jwt token - without Bearer and a space

            CartEntity cart = await cartServices.AddCart(quantity,bookID,userID,token);

            if(cart != null)
            {
                return Ok(new ResponseModel<CartEntity> { Status = true, Message = "succesfull to add Cart", Data = cart });
            }

            return BadRequest(new ResponseModel<bool> { Status = false, Message = "unsuccesfull to add Cart" });
        }

        [Authorize]
        [HttpDelete("removeCartByBookID")]
        public IActionResult RemoveCart(int bookID)
        {
            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));

            bool isRemoved = cartServices.RemoveCart(bookID,userID);
            if(isRemoved)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "succesfull to remove from cart" });
            }
            return BadRequest(new ResponseModel<bool> { Status = true, Message = "unsuccesfull to remove from cart" });
        }
    }
}
