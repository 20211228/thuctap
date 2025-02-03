using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.repository
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
      
        public Task<T> GetById(int id);
        public Task Add(T T);
        public Task Update(T T);
        public Task Delete(T T);

        public Task SaveChange();
        
    }
}
