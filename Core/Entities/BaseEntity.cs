using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}
