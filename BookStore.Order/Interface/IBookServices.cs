namespace BookStore.Order.Interface
{
    public interface IBookServices
    {
        Task<BookEntity> GetBookDetails(int id);
    }
}
