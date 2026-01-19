using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Teacher;

public class CreatedTeacher
{
    [Required]
    public required string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    [Phone]
    public required string Phone { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
}