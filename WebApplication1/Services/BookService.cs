using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.repository;

namespace WebApplication1.Services
{
    public class BookService:IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository,IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            
        }

        public async Task<int> CreateBook(BookModel book)
        {
            var newbook = _mapper.Map<Book>(book);
            await _bookRepository.Addbook(newbook);
            await  _bookRepository.SaveChange();
            return newbook.Id;
            
        }

        public async Task DeleteBook(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book != null)
            {
                await _bookRepository.DeleteBook(id);
                
            }
            await _bookRepository.SaveChange();
        }

        public async Task<List<BookDTO>> GetAllBook()
        {
            var book =   _bookRepository.GetAllBooks();
            var result = book.Select(p=> new BookDTO { Id = p.Id, Title = p.Title }).ToList();
            return result;
        }

        public async Task<BookDTO> GetBookById(int id)
        {
            var book =await _bookRepository.GetBookById(id);
            var result = new BookDTO { Id = book.Id, Title = book.Title };
            return result;
        }

        public async Task UpdateBook(int id, BookModel book)
        {

            var updatebook = _mapper.Map<Book>(book);
            updatebook.Id = id;
            await _bookRepository.UpdateBook( updatebook);
            await _bookRepository.SaveChange();
        }
    }
}
