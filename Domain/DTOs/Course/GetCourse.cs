using System.ComponentModel.DataAnnotations;
using Domain.DTOs.Group;
using Domain.DTOs.Teacher;

namespace Domain.DTOs.Course;

public class GetCourse
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required]
    public required string Title { get; set; }
    [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TeacherId { get; set; }
    
    public GetTeacher? Teacher { get; set; }
    public List<GetGroup>? Groups { get; set; }
}