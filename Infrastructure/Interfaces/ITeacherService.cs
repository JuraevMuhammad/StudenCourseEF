using Domain.DTOs.Teacher;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ITeacherService
{
    Response<List<GetTeacher>> GetTeachers();
    Response<GetTeacher> GetTeacher(int id);
    Response<string> CreatedTeacher(CreatedTeacher dto);
    Response<string> UpdateTeacher(int id, UpdateTeacher dto);
    Response<string> DeleteTeacher(int id);
    Response<List<GetTeacherWithCourses>> GetTeachersWithCourses();
    Response<List<GetTeacher>> GetTeachersSearchName(string searchName);

}