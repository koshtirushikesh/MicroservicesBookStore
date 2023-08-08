namespace BookStore.Order.Interface
{
    public interface IUserServices
    {
        Task<UserEntity> GetUserDetails(string token);
    }
}
