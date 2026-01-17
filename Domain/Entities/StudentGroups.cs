namespace Domain.Entities;

public class StudentGroups : BaseEntities
{
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    
    public Student? Students { get; set; }
    public Group? Groups { get; set; }
}

