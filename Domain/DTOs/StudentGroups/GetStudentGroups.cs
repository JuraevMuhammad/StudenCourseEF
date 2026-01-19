using Domain.DTOs.Group;
using Domain.DTOs.Student;

namespace Domain.DTOs.StudentGroups;

public class GetStudentGroups
{
    public int Id { get; set; }
    public GetStudent? Students { get; set; }
    public GetGroup? Groups { get; set; }
}