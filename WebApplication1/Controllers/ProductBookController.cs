using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.repository;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductBookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public ProductBookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBook()
        {
            var book = await _bookService.GetAllBook();

            return Ok(book);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById([FromRoute]int id)
        {
            var book =  await _bookService.GetBookById(id);
            if(book == null)
            {
                return BadRequest();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody]BookModel book)
        {
            var newbook = await _bookService.CreateBook(book);
            var bookresult = await _bookService.GetBookById(newbook.Item);
            if(bookresult == null)
            {
                return BadRequest();
            }
            return Ok(bookresult);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditBook([FromRoute]int id,[FromBody] BookModel book)
        {
            try
            {
                var updatebook = await _bookService.UpdateBook(id, book);
                return Ok(updatebook);
            }
            catch(Exception ex) {
            
                return BadRequest(ex);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute]int id)
        {
            await _bookService.DeleteBook(id);
            return NoContent();
        }

    }
}
