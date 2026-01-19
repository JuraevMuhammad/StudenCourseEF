using System.ComponentModel.DataAnnotations;
using Domain.DTOs.Group;
using Domain.DTOs.Teacher;

namespace Domain.DTOs.Course;

public class GetCourse
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TeacherId { get; set; }
    
    public GetTeacher? Teacher { get; set; }
}