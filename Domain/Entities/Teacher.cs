using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Teacher : BaseEntities
{
    [Required]
    public required string FullName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    [Phone]
    public required string Phone { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    
    public List<Course>? Courses { get; set; }
}