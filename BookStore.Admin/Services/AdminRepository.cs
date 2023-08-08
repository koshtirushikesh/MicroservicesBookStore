using BookStore.Admin.Entity;
using BookStore.Admin.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Admin.Services
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AdminDBContext adminDBContext;
        private readonly IConfiguration _config;
        public AdminRepository(AdminDBContext adminDBContext, IConfiguration config)
        {
            this.adminDBContext = adminDBContext;
            _config = config;
        }

        public AdminEntity AddAdmin(AdminEntity admin)
        {
            adminDBContext.Admin.Add(admin);
            adminDBContext.SaveChanges();
            return admin;
        }

        public string AdminLogin(AdminEntity adminEntity)
        {
            AdminEntity login = adminDBContext.Admin.Where(x => x.Email == adminEntity.Email && x.Password == adminEntity.Password).FirstOrDefault();

            if (login != null)
            {
                return GenerateToken(login.Email, login.AdminID);
            }
            return null;
        }

        public string GenerateToken(string email, int adminID)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",email),
                new Claim("AdminID",adminID.ToString()),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
