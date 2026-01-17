using System.ComponentModel.DataAnnotations;
using Domain.DTOs.Course;
using Domain.DTOs.StudentGroups;

namespace Domain.DTOs.Group;

public class GetGroup
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; } = string.Empty;
    public int CourseId  { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsStarted { get; set; }
    
    public GetCourse? Course { get; set; }
    public List<GetStudentGroups>? StudentGroups { get; set; }
}

