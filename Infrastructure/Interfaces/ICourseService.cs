using Domain.DTOs.Course;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICourseService
{
    Response<List<GetCourse>> GetAllCourse();
    Response<GetCourse> GetCourse(int id);
    Response<List<GetCourse>> GetAllCoursesWithTeacher();
    Response<string> CreatedCourse(CreatedCourse dto);
    Response<string> UpdateCourse(int id, UpdateCourse dto);
    Response<string> DeleteCourse(int id);
    Response<List<GetCourse>> GetCoursesFilterPrice(decimal minPrice, decimal maxPrice);
}