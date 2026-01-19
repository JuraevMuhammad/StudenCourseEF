using System.Net;
using Domain.DTOs.Course;
using Domain.DTOs.Teacher;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService(ApplicationDbContext context) : ICourseService
{
    public Response<List<GetCourse>> GetAllCourse()
    {
        var courses = context.Courses.Where(x => x.IsDeleted == false)
            .OrderBy(x => x.CreatedAt).ToList();
        var res = courses.Select(x => new GetCourse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                CreatedAt = x.CreatedAt,
                TeacherId = x.TeacherId,
            }
        ).ToList();
        return new Response<List<GetCourse>>(res);
    }

    public Response<GetCourse> GetCourse(int id)
    {
        var course = context.Courses.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        if (course == null)
            return new Response<GetCourse>(HttpStatusCode.NotFound, "Course not found");
        var res = new GetCourse()
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Price = course.Price,
            CreatedAt = course.CreatedAt,
            TeacherId = course.TeacherId,
        };
        return new Response<GetCourse>(res);
    }

    public Response<List<GetCourse>> GetAllCoursesWithTeacher()
    {
        var courses = context.Courses.Include(x=> x.Teacher).Where(x => x.IsDeleted == false).OrderBy(x => x.CreatedAt).ToList();
        var res = courses.Select(x => new GetCourse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                CreatedAt = x.CreatedAt,
                TeacherId = x.TeacherId,
                Teacher = new GetTeacher()
                {
                    Id = x.Teacher!.Id,
                    FirstName = x.Teacher.FirstName,
                    LastName = x.Teacher.LastName,
                    Email = x.Teacher.Email,
                    Phone = x.Teacher.Phone,
                    Specialization = x.Teacher.Specialization,
                    ExperienceYears = x.Teacher.ExperienceYears,
                }

            }
        ).ToList();
        return new Response<List<GetCourse>>(res);
    }

    public Response<string> CreatedCourse(CreatedCourse dto)
    {
        var res = new Course()
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            TeacherId = dto.TeacherId,
        };
        context.Courses.Add(res);
        var result = context.SaveChanges();
        return result > 0
            ? new Response<string>(HttpStatusCode.Created,$"Course {res.Id} created successfully")
            : new Response<string>(HttpStatusCode.BadRequest,$"Course {res.Id} could not be created");
    }

    public Response<string> UpdateCourse(int id, UpdateCourse dto)
    {
        var course = context.Courses.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        if (course == null)
            return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        course.Title = dto.Title ?? course.Title;
        course.Description = dto.Description ?? course.Description;
        course.Price = dto.Price ?? course.Price;
        course.TeacherId = dto.TeacherId ?? course.TeacherId;
        var res =context.SaveChanges();
        return res > 0
            ? new Response<string>(HttpStatusCode.Accepted, "Course updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Course could not be updated");
    }

    public Response<string> DeleteCourse(int id)
    {
        var res = context.Courses.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        res.IsDeleted = true;
        context.SaveChanges();
        return new Response<string>(HttpStatusCode.Accepted, "Course deleted successfully");
    }

    public Response<List<GetCourse>> GetCoursesFilterPrice(decimal minPrice, decimal maxPrice)
    {
        var res = context.Courses.Where(x => x.Price >= minPrice && x.Price >= maxPrice).ToList();
        var result = res.Select(x => new GetCourse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                CreatedAt = x.CreatedAt,
                TeacherId = x.TeacherId,
            }
        ).ToList();
        return new Response<List<GetCourse>>(result);
    }
}