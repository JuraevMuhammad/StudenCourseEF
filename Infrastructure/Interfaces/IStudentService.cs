using Domain.DTOs.Student;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IStudentService
{
    Response<List<GetStudent>> GetStudents();
    Response<GetStudent> GetStudentById(int id);
    Response<string> CreatedStudent(CreatedStudent dto);
    Response<string> UpdateStudent(int id, UpdateStudent dto);
    Response<string> DeleteStudent(int id);
    Response<List<GetStudent>> GetStudentsDeleted();
    Response<string> RestoreStudent(int id);
}