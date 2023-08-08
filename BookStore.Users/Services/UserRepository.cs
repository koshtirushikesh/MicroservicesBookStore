
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext userDBContext;
        private readonly IConfiguration _config;
        public UserRepository(UserDBContext userDBContext, IConfiguration config)
        {
            this.userDBContext = userDBContext;
            _config = config;
        }
        public UserEntity AddUser(UserEntity user)
        {

            userDBContext.Add(user);
            userDBContext.SaveChanges();

            return user;
        }

        public string LoginUser(UserEntity user)
        {
            UserEntity result = userDBContext.User.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (result != null)
            {
                return GenerateToken(result.Email,result.UserID);
            }

            return null;
        }

        public string ForgetPassword(string email)
        {
            var result = userDBContext.User.Where(x => x.Email == email).FirstOrDefault();

            if (result != null)
            {
                string token = GenerateToken(result.Email, result.UserID);

                return token;
            }

            return null;
        }

        public UserEntity ResetPassword(string password, string confirmPassword, string email)
        {
            var result = userDBContext.User.Where(x => x.Email == email).FirstOrDefault();
            if (result != null)
            {
                result.Password = confirmPassword;
                userDBContext.SaveChanges();
                return result;
            }
            return null;
        }

        public UserEntity GetUserProfile(int userID)
        {
            var result = userDBContext.User.Where(x => x.UserID == userID).FirstOrDefault();
            return result;
        }

        private string GenerateToken(string email,int userID)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",email),
                new Claim("UserID",userID.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
