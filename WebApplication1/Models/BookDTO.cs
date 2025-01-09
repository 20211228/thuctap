using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
    }
}
