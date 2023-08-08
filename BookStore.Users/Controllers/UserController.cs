using BookStore.Admin.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("add-user")]
        public IActionResult AddUser(UserEntity user)
        {
            UserEntity userEntity = userRepository.AddUser(user);

            if (userEntity != null)
            {
                return Ok(new ResponseModel<UserEntity> { Status = true, Message = "succesfully added user", Data = userEntity });
            }

            return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "unable add user" });
        }

        [HttpPut("login-user")]
        public IActionResult UserLogin(UserEntity user)
        {
            string token = userRepository.LoginUser(user);

            if (token != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "succesfully login", Data = token });
            }

            return NotFound(new ResponseModel<string> { Status = false, Message = "unsuccesfull login" });
        }

        [HttpPut("user-forget-password")]
        public IActionResult ForgetPassword(string Email)
        {
            Send send = new Send();
            string token = userRepository.ForgetPassword(Email);

            if (token != null)
            {
                send.SendEmail(Email, token);
                return Ok(new ResponseModel<string> { Status = true, Message = "succesfully send token to email", Data = token });
            }

            return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to send email" });
        }

        [Authorize]
        [HttpPut("reset-password")]
        public IActionResult ResetPassword(string Password, string confirmPassword)
        {
            string Email = User.FindFirst("Email").Value;
            if (Password == confirmPassword)
            {
                UserEntity user = userRepository.ResetPassword(Password, confirmPassword, Email);
                if (user != null)
                {
                    return Ok(new ResponseModel<string> { Status = true, Message = "password change succesfull" });
                }
                return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to change password" });

            }
            return BadRequest(new ResponseModel<string> { Status = false, Message = "password is not matching with confirm password" });
        }

        [Authorize]
        [HttpGet("getUserProfile")]
        public IActionResult GetUserProfile()
        {
            int userID = Convert.ToInt32(User.FindFirst("UserID").Value);
            if (userID != null)
            {
                UserEntity user = userRepository.GetUserProfile(userID);
                return Ok(new ResponseModel<UserEntity> { Status = true, Message = "user details featched succesfully", Data = user });

            }
            return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to get user details" });
        }
    }
}
