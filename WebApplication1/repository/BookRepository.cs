using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;
        

        public BookRepository(BookContext bookContext)
        {
            _context = bookContext;
           
        }

        public async Task Addbook(Book book)
        {  
            _context.Books.Add(book);
            await Task.CompletedTask;
        }

        public async Task DeleteBook(int id)
        {
            var deletebook = _context.Books.FirstOrDefault(x => x.Id == id);
            if (deletebook != null)
            {
                 _context.Books.Remove(deletebook);
                await Task.CompletedTask;
            }
        }

        public IQueryable<Book> GetAllBooks()
        {
            
            return  _context.Books;
            
            
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
             
         
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {       
            _context.Books.Update(book);
            await Task.CompletedTask;

        }
    }
}
