using BookStore.Products.Entity;

namespace BookStore.Products.service
{
    public class BookRepository : IBookRepository
    {
        private readonly ProductDBContext productDBContext;

        public BookRepository(ProductDBContext productDBContext)
        {
            this.productDBContext = productDBContext;
        }


        public BookEntity AddBook(BookEntity bookEntity)
        {
            productDBContext.Book.Add(bookEntity);
            productDBContext.SaveChanges();

            return bookEntity;
        }

        public bool DeleteBook(int BookID)
        {
            BookEntity book = productDBContext.Book.Where(x => x.BookID == BookID).FirstOrDefault();

            if (book != null)
            {
                productDBContext.Book.Remove(book);
                productDBContext.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<BookEntity> GetAllBooks()
        {
            IEnumerable<BookEntity> books = productDBContext.Book;

            if (books != null)
            {
                return books;
            }

            return null;
        }

        public BookEntity GetBookByID(int BookID)
        {
            BookEntity books = productDBContext.Book.Where(x => x.BookID == BookID).FirstOrDefault();

            if (books != null)
            {
                return books;
            }

            return null;
        }

        public BookEntity UpdateBook(BookEntity bookEntity, int bookID)
        {
            if (bookEntity != null)
            {
                BookEntity book = productDBContext.Book.Where(x => x.BookID == bookID).FirstOrDefault();
                book.Name = bookEntity.Name;
                book.Description = bookEntity.Description;
                book.Author = bookEntity.Author;
                book.Quantity = bookEntity.Quantity;
                book.discountedprice = bookEntity.discountedprice;
                book.actualprice = bookEntity.actualprice;

                productDBContext.Book.Update(book);
                productDBContext.SaveChanges();

                return book;
            }

            return null;
        }
    }
}
