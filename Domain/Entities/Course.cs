using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Course : BaseEntities
{
    [Required]
    public required string Title { get; set; }
    [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TeacherId { get; set; }
    
    public Teacher? Teacher { get; set; }
    public List<Group>? Groups { get; set; }
}