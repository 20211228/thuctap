using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
    }


    public class apiResult<T>
    {
        public string[] msg { get; set; } = [];
        public T Item { get; set; }
        public apiResult(string[] msg, T item)
        {
            this.msg = msg;
            Item = item;
        }
    }
}
