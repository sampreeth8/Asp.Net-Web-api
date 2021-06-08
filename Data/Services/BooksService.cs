using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Book
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead?book.Rate.Value:null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl
                

            };

            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int Id)
        {
            var model = _context.Books.FirstOrDefault(p => p.Id == Id);
            return model;
        }

        public Book UpdateBookById(int id,BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(p => p.Id == id);
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
            }

            return _book;
        }

        public string DeleteBookById(int id)
        {
            var book = _context.Books.FirstOrDefault(p => p.Id == id);
            if(book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();

                return "Succesfully deleted";

            }
            else
            {
                return "Cannot Find the book with given id";
            }
            
        }
    }
}
