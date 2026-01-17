using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Group : BaseEntities
{
    [Required]
    public required string Name { get; set; } = string.Empty;
    public int CourseId  { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsStarted { get; set; }
    public int Grade { get; set; }
    
    public Course? Course { get; set; }
    public List<StudentGroups>? StudentGroups { get; set; }
}