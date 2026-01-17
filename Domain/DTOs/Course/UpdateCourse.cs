using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Course;

public class UpdateCourse
{
    [Required]
    public required string? Title { get; set; }
    [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? TeacherId { get; set; }
}