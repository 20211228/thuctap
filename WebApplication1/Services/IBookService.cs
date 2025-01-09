using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IBookService
    {
        public Task<List<BookDTO>> GetAllBook();
        public Task<BookDTO> GetBookById(int id);
        public Task<int> CreateBook(BookModel book);
        public Task UpdateBook(int id, BookModel book);
        public Task DeleteBook(int id);
    }
}
