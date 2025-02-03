using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class BaseEntityByKey <TKey> : BaseEntity
    {
        [Key]
        public TKey Id { get; set; }
    }
}
