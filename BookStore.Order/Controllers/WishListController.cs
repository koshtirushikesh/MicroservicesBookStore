using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService wishListServices;
        public WishListController(IWishListService wishListServices)
        {
            this.wishListServices = wishListServices;
        }

        [Authorize]
        [HttpPost("addWishList")]
        public async Task<IActionResult> AddWishList(int bookID)
        {
            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));

            string token = Request.Headers.Authorization.ToString(); // token will have "Bearer " which we need to remove
            token = token.Substring("Bearer ".Length); // now we will only have the actual jwt token - without Bearer and a space

            WishListEntity wishList = await wishListServices.AddWishList(bookID, userID,token);
            if(wishList != null)
            {
                return Ok(new ResponseModel<WishListEntity> { Status = true, Message = "succesfully added to wish list", Data = wishList });
            }

            return BadRequest(new ResponseModel<string> { Status = false, Message = "unsuccesfull to add wish list" });
        }
    }
}
