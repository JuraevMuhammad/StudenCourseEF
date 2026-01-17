using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Student : BaseEntities
{
    [Required]
    public required string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
    [Phone]
    public string Phone { get; set; } = string.Empty;
    
    public List<StudentGroups>? StudentGroups { get; set; }
}