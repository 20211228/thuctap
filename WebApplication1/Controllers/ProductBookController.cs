using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.repository;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public ProductBookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                return Ok(await _bookService.GetAllBook());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book =  await _bookService.GetBookById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookModel book)
        {
            var newbook = await _bookService.CreateBook(book);
            var bookresult = await _bookService.GetBookById(newbook);
            return bookresult == null ? NotFound() : Ok(newbook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, BookModel book)
        {
            await _bookService.UpdateBook(id, book);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return Ok();
        }

    }
}
