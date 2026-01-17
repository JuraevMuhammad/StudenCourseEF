using Domain.DTOs.StudentGroups;

namespace Domain.DTOs.Student;

public class GetStudent
{
    public int Id { get; set; }
    public required string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    
    public List<GetStudentGroups>? StudentGroups { get; set; }
}