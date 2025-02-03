using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public abstract class BaseEntity
    {
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
