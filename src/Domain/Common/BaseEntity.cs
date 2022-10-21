using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Common;

public abstract class BaseEntity
{
    public string? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
