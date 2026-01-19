using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Course;

public class UpdateCourse
{ 
    public string? Title { get; set; } 
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? TeacherId { get; set; }
}