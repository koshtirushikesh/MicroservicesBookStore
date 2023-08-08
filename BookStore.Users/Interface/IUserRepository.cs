namespace BookStore.Users
{
    public interface IUserRepository
    {
        public UserEntity AddUser(UserEntity UserEntity);
        public string ForgetPassword(string email);
        public string LoginUser(UserEntity user);
        public UserEntity ResetPassword(string password, string confirmPassword, string email);
        public UserEntity GetUserProfile(int userID);
    }
}
