namespace Domain.DTOs.Teacher;

public class UpdateTeacher
{
    public string? FullName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public required string? Phone { get; set; }
    public string? Specialization { get; set; }
    public int? ExperienceYears { get; set; }
}
