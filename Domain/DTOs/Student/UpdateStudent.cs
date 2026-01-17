using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Student;

public class UpdateStudent
{
    public required string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string? Email { get; set; } = string.Empty;
    [Phone]
    public string? Phone { get; set; } = string.Empty;
}