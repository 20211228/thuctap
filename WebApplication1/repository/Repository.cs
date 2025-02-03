using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BookContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(BookContext bookContext)
        {
            _context = bookContext;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {  
            _dbSet.Add(entity);
        }


        public async Task Delete(T entity)
        {
     
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await Task.CompletedTask;
            }
        }

        

        public IQueryable<T> GetAll()
        {
            
            return _dbSet.AsQueryable();
            
            
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {       
            _dbSet.Update(entity);
            await Task.CompletedTask;

        }


      
    }
}
