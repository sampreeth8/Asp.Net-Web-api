using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
     
        public BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("get-allbooks")]
        public IActionResult GetAllBooks()
        {
            var books = _booksService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("get-bookbyid/{Id}")]
        public IActionResult GetBookById(int Id)
        {
            var book = _booksService.GetBookById(Id);
            if (book == null)
            {
                return NotFound("Did not found any book with given book id");
            }
            return Ok(book);
        }


        [HttpPost("add-book-with-Author-Publisher")]
        public IActionResult AddBook(BookVM book)
        {
            _booksService.AddBook(book);
            return Ok();
        }

        [HttpPut("update-bookbyid/{id}")]
        public IActionResult UpdateBookById(int id,[FromBody] BookVM book)
        {
            var _book = _booksService.UpdateBookById(id, book);
            return Ok(_book);
        }

        [HttpDelete("delete-bookbyid/{id}")]

        public IActionResult DeleteBookById(int id)
        {
            var result = _booksService.DeleteBookById(id);
            return Ok(result);
        }

    }
}
