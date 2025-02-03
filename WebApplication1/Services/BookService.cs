using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.repository;

namespace WebApplication1.Services
{
    public interface IBookService
    {
        public Task<apiResult<List<BookDTO>>> GetAllBook();
        public Task<apiResult<BookDTO>> GetBookById(int id);
        public Task<apiResult<int>> CreateBook(BookModel book);
        public Task<apiResult<BookDTO>> UpdateBook(int id, BookModel book);
        public Task<apiResult<BookDTO>> DeleteBook(int id);
    }


    public class BookService:IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IRepository<Book> bookRepository,IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            
        }

        public async Task<apiResult<int>> CreateBook(BookModel book)
        {
            try
            {
                var newbook = _mapper.Map<Book>(book);
                await _bookRepository.Add(newbook);

                await _bookRepository.SaveChange();
                return new apiResult<int>(new[] {"Thêm thành công"},newbook.Id);
            }
            catch (Exception ex)
            {
                return new apiResult<int>(new[] {ex.ToString()},0);
            }
        }

        public async Task<apiResult<BookDTO>> DeleteBook(int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book != null)
            {
                await _bookRepository.Delete(book);
                await _bookRepository.SaveChange();
                return new apiResult<BookDTO>(new[] { "đã xoá" },new BookDTO { Id = book.Id,Title = book.Title});
            }
            else
            {
                return new apiResult<BookDTO>(new[] { "Không có bản ghi" }, null);
            }
        }

        public async Task<apiResult<List<BookDTO>>> GetAllBook()
        {
            var book = await _bookRepository.GetAll()
                .Select(p=> new BookDTO { Id = p.Id, Title = p.Title }).ToListAsync();
            if(book != null)
            {
                return new apiResult<List<BookDTO>>(new[] {"thanh cong"},book );
            }
            return new apiResult<List<BookDTO>>(new[] {"khong co ban ghi"},null);
            
        }

        public async Task<apiResult<BookDTO>> GetBookById(int id)
        {
            var book =await _bookRepository.GetById(id);
            if (book != null)
            {
                var result = new BookDTO { Id = book.Id, Title = book.Title };
                return new apiResult<BookDTO>(new[] {"tìm thay"},result);
            }
            return new apiResult<BookDTO>(new[] { "khong co ban ghi" }, null);
            
        }

        public async Task<apiResult<BookDTO>> UpdateBook(int id, BookModel book)
        {
            var updatebook = await _bookRepository.GetById(id);
            if (updatebook != null)
            {
                _mapper.Map(book, updatebook);
                await _bookRepository.Update(updatebook);
                await _bookRepository.SaveChange();
                return new apiResult<BookDTO> ( new[] { "Cập nhật thanh công" }, new BookDTO { Id = updatebook.Id, Title = updatebook.Title} );
            }
            else
            {
                return new apiResult<BookDTO>(new[] { "cập nhật thất bại" },null );
            }
        }
    }
}
