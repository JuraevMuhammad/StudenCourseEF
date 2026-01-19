using Domain.DTOs.StudentGroups;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IStudentGroupsService
{
    Response<string> AddStudentInGroup(AddStudentGroups dto);
    Response<string> RemoveStudent(int id);
    Response<List<GetStudentGroups>> GetAllStudents(int studentId);
}