using Domain.DTOs.Group;

namespace Domain.DTOs.Student;

public class GetStudent
{
    public int Id;
    public required string FirstName = string.Empty;
    public string LastName = string.Empty;
    public DateTime BirthDate;
    public string Email = string.Empty;
    public string Phone = string.Empty;
    
    public List<GetGroup>? StudentGroups { get; set; }
}