using BookStore.Products.Entity;

namespace BookStore.Products.service
{
    public interface IBookRepository
    {
        public BookEntity AddBook(BookEntity bookEntity);
        public BookEntity GetBookByID(int BookID);
        public IEnumerable<BookEntity> GetAllBooks();
        public BookEntity UpdateBook(BookEntity bookEntity, int bookID);
        public bool DeleteBook(int BookID);
    }
}
