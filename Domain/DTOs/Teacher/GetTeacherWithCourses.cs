using Domain.DTOs.Course;

namespace Domain.DTOs.Teacher;

public class GetTeacherWithCourses
{
    public int Id { get; set; }
    public required string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    
    public List<GetCourse>? Courses { get; set; }
}