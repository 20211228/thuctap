using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.repository
{
    public interface IBookRepository
    {
        public IQueryable<Book> GetAllBooks();
      
        public Task<Book> GetBookById(int id);
        public Task Addbook(Book book);
        public Task UpdateBook(Book book);
        public Task DeleteBook(int id);

        public Task SaveChange();
        
    }
}
