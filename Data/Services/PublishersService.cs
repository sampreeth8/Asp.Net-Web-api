using my_books.Data.Models;
using my_books.Data.Paging;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int id)
        {
            var _publisher = _context.Publishers.Where(n => n.Id == id).Select(n => new PublisherWithBooksAndAuthorsVM()
            {
                Name = n.Name,
                BookAuthors = n.Books.Select( n=> new BookAuthorVM()
                {
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(n=>n.Author.FullName).ToList()

                }).ToList()
            }).FirstOrDefault();

            return _publisher;

        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n=>n.Id == id);
            if(_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"the publisher with given id:{id} does not exist");
            }
        }

        public List<Publisher> GetAllPublishers(string sortBy, string searchString,int? pageNumber)
        {
            var model = _context.Publishers.ToList();

            if(!string.IsNullOrEmpty(sortBy))
            {
                switch(sortBy)
                {
                    case "id_desc":
                        model = model.OrderByDescending(n => n.Id).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // Paging
            int pageSize = 5;
            model = PaginatedList<Publisher>.Create(model.AsQueryable(), pageNumber ?? 1, pageSize);

          
            return model;
        }




    }
}
