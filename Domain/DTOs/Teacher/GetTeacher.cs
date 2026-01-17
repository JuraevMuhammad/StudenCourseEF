using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Teacher;

public class GetTeacher
{
    public int Id { get; set; }
    public required string FullName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
}