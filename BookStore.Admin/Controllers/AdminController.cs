using BookStore.Admin.Entity;
using BookStore.Admin.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository adminRepository;
        public AdminController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        [HttpPost("add-admin")]
        public IActionResult AddAdmin(AdminEntity adminEntity)
        {
            AdminEntity admin = adminRepository.AddAdmin(adminEntity);

            if (admin != null)
            {
                return Ok(new ResponceModel<AdminEntity> { Status = true, Message = "succesfully to added admin", Data = admin });
            }

            return BadRequest(new ResponceModel<string> { Status = false, Message = "unsuccesfull to add admin" });
        }

        [HttpPost("admin-login")]
        public IActionResult AdminLogin(AdminEntity adminEntity)
        {
            string loginToken = adminRepository.AdminLogin(adminEntity);

            if (loginToken != null)
            {
                return Ok(new ResponceModel<string> { Status = true, Message = "admin login succesfull", Data = loginToken });
            }

            return NotFound(new ResponceModel<string> { Status = false, Message = "admin login unsuccesfull", Data = loginToken });
        }
    }
}
