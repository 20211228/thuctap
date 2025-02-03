using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    
    public class Book : BaseEntityByKey<int>
    {

        public string Title { get; set; }
        public string Description { get; set; }
        
        public double Price { get; set; }
        
        public int Quantity { get; set; }

    }
}
