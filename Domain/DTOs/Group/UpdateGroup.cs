using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Group;

public class UpdateGroup
{
    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
    public required string? Name { get; set; }
    public int? CourseId  { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsStarted { get; set; }
}