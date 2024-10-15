using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public abstract class SoftDeletableEntity : BaseEntity
    {
        public bool IsDelete { get; set; } = false;

        [Column(TypeName = "datetime")]
        public DateTime? DeletedDate { get; set; }
    }
}
