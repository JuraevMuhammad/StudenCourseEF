using System.Net;
using Domain.DTOs.Course;
using Domain.DTOs.Teacher;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TeacherService(ApplicationDbContext context) : ITeacherService
{
    public Response<List<GetTeacher>> GetTeachers()
    {
        var res = context.Teachers.Where(x => x.IsDeleted == false).OrderBy(x => x.CreatedAt).ToList();
        var getTeachers = res.Select(x => new GetTeacher()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                Specialization = x.Specialization,
                ExperienceYears = x.ExperienceYears,
            }
        ).ToList();
        return new Response<List<GetTeacher>>(getTeachers);
    }

    public Response<GetTeacher> GetTeacher(int id)
    {
        var course = context.Teachers.Find(id);
        if (course == null)
            return new Response<GetTeacher>(HttpStatusCode.NotFound, "not found");
        var res = new GetTeacher()
        {
            Id = course.Id,
            FirstName = course.FirstName,
            LastName = course.LastName,
            Email = course.Email,
            Phone = course.Phone,
            Specialization = course.Specialization,
            ExperienceYears = course.ExperienceYears,
        };
        return new Response<GetTeacher>(res);
    }

    public Response<string> CreatedTeacher(CreatedTeacher dto)
    {
        var createdTeacher = new Teacher()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Specialization = dto.Specialization,
            ExperienceYears = dto.ExperienceYears,
        };
        context.Teachers.Add(createdTeacher);
        var res = context.SaveChanges();
        return res > 0
            ? new Response<string>(HttpStatusCode.Created, "CreatedTeacher")
            : new Response<string>(HttpStatusCode.BadRequest, "Error");
    }

    public Response<string> UpdateTeacher(int id, UpdateTeacher dto)
    {
        var res = context.Teachers.Find(id);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        res.FirstName = dto.FirstName ?? res.FirstName;
        res.LastName = dto.LastName ?? res.LastName;
        res.Email = dto.Email ?? res.Email;
        res.Phone = dto.Phone ?? res.Phone;
        res.Specialization = dto.Specialization ?? res.Specialization;
        res.ExperienceYears = dto.ExperienceYears ?? res.ExperienceYears; 
        var result = context.SaveChanges();
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "UpdatedTeacher")
            : new Response<string>(HttpStatusCode.BadRequest, "Error");
    }

    public Response<string> DeleteTeacher(int id)
    {
        var res = context.Teachers.FirstOrDefault(x => x.Id == id);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        res.IsDeleted = true;
        var result = context.SaveChanges();
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "DeletedTeacher")
            : new Response<string>(HttpStatusCode.BadRequest, "Error");
    }

    public Response<List<GetTeacherWithCourses>> GetTeachersWithCourses()
    {
        var getCourse = context.Teachers.Include(x => x.Courses)
            .Where(x => x.IsDeleted == false).OrderBy(x => x.CreatedAt).ToList();
        
        var res = getCourse.Select(x => new GetTeacherWithCourses()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                Specialization = x.Specialization,
                ExperienceYears = x.ExperienceYears,
                Courses = x.Courses!.Select(c => new GetCourse()
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        CreatedAt = x.CreatedAt,
                        Price = c.Price,
                        TeacherId = c.TeacherId,
                    }
                ).ToList()
            }
        ).ToList();
        return new Response<List<GetTeacherWithCourses>>(res);
    }

    public Response<List<GetTeacher>> GetTeachersSearchName(string searchName)
    {
        var res = context.Teachers.Where(x =>
            x.FirstName.Concat(x.LastName).ToString().ToUpper().Contains(searchName.ToUpper())).ToList();
        var get = res.Select(x => new GetTeacher()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                Specialization = x.Specialization,
                ExperienceYears = x.ExperienceYears,
            }
        ).ToList();
        return new Response<List<GetTeacher>>(get);
    }
}