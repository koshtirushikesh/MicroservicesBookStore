using BookStore.Products.Entity;
using BookStore.Products.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-book")]
        public IActionResult AddBook(BookEntity bookEntity)
        {
            BookEntity book = bookRepository.AddBook(bookEntity);
            if (book != null)
            {
                return Ok(new ResponseModel<BookEntity> { Status = true, Message = "book added succesfully", Data = book });
            }

            return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to add book" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteBook")]
        public IActionResult DeleteBook(int BookID)
        {
            bool result = bookRepository.DeleteBook(BookID);
            if (result)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "book deleted" });
            }

            return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to delete book" });
        }


        [HttpGet("get-BybookId")]
        public IActionResult GetBookByID(int bookID)
        {
            BookEntity book = bookRepository.GetBookByID(bookID);
            if (book != null)
            {
                return Ok(new ResponseModel<BookEntity> { Status = true, Message = "succesfully get books details", Data = book });
            }
            return NotFound(new ResponseModel<string> { Status = false, Message = "book not found" });
        }

        [HttpGet("getAllBooks")]
        public IActionResult GetAllBooks()
        {
            IEnumerable<BookEntity> books = bookRepository.GetAllBooks();
            if (books != null)
            {
                return Ok(new ResponseModel<IEnumerable<BookEntity>> { Status = true, Message = "successfully get all book details", Data = books });
            }

            return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to get books" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateByBookID")]
        public IActionResult UpdateBook(BookEntity bookEntity, int bookID)
        {
            BookEntity book = bookRepository.UpdateBook(bookEntity, bookID);

            if (book != null)
            {
                return Ok(new ResponseModel<BookEntity> { Status = true, Message = "successfully updated book", Data = book });
            }
            return BadRequest(new ResponseModel<string> { Status = false, Message = "unable to update book" });
        }
    }
}
