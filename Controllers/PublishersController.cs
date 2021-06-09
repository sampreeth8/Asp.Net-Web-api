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
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
       
        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
           
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy,string searchString, int pageNumber)
        {
            var response = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(response);
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            var response = _publishersService.AddPublisher(publisher);
            return Created(nameof(AddPublisher), response);
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete - publisher - by - id /{id}")]
        public IActionResult DeltePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }


}
