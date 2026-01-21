using System.Net;
using Domain.DTOs.Student;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class StudentService(ApplicationDbContext context) : IStudentService
{
    #region GetStudents

    public Response<List<GetStudent>> GetStudents()
    {
        var res = context.Students.Where(x => x.IsDeleted == false).ToList();
        if(res.Count == 0)
            return new Response<List<GetStudent>>(HttpStatusCode.NotFound, "Student not found");
        
        var get = res.Select(x => new GetStudent()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                BirthDate = x.BirthDate,
            }
        ).ToList();
        return new Response<List<GetStudent>>(get);
    }

    #endregion

    #region GetStudentById

    public Response<GetStudent> GetStudentById(int id)
    {
        var res = context.Students.FirstOrDefault(x => x.Id == id);
        if (res == null)
            return new Response<GetStudent>(HttpStatusCode.NotFound, "Student not found");

        var get = new GetStudent()
        {
            Id = res.Id,
            FirstName = res.FirstName,
            LastName = res.LastName,
            Email = res.Email,
            Phone = res.Phone,
            BirthDate = res.BirthDate,
        };
        return new Response<GetStudent>(get);
    }

    #endregion

    #region CreatedStudent

    public Response<string> CreatedStudent(CreatedStudent dto)
    {
        var created = new Student()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            BirthDate = dto.BirthDate,
        };
        context.Students.Add(created);
        var res = context.SaveChanges();
        return res > 0
            ? new Response<string>(HttpStatusCode.Created, "Student created")
            : new Response<string>(HttpStatusCode.BadRequest, "no created");
    }

    #endregion

    #region UpdateStudent

    public Response<string> UpdateStudent(int id, UpdateStudent dto)
    {
        var res = context.Students.FirstOrDefault(x => x.Id == id);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "Student not found");
        res.FirstName = dto.FirstName ?? res.FirstName;
        res.LastName = dto.LastName ?? res.LastName;
        res.Email = dto.Email ?? res.Email;
        res.Phone = dto.Phone ?? res.Phone;
        res.BirthDate = dto.BirthDate ?? res.BirthDate;
        
        var result = context.SaveChanges();
        return result > 0
            ? new Response<string>(HttpStatusCode.NoContent, "Student updated")
            : new Response<string>(HttpStatusCode.BadRequest, "no updated student");
    }

    #endregion

    #region DeleteStudent

    public Response<string> DeleteStudent(int id)
    {
        var res = context.Students.FirstOrDefault(x => x.Id == id);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "Student not found");
        res.IsDeleted = true;
        var result = context.SaveChanges();
        return result > 0
            ? new Response<string>(HttpStatusCode.NoContent, "Student deleted")
            : new Response<string>(HttpStatusCode.BadRequest, "no deleted student");
    }

    #endregion

    #region GetStudentsDeleted

    public Response<List<GetStudent>> GetStudentsDeleted()
    {
        var res = context.Students
            .Where(x => x.IsDeleted != false).ToList();
        if(res.Count == 0)
            return new Response<List<GetStudent>>(HttpStatusCode.NotFound, "Student not found");
        
        var get = res.Select(x => new GetStudent()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                BirthDate = x.BirthDate,
            }
        ).ToList();
        return new Response<List<GetStudent>>(get);
    }

    #endregion

    #region RestoreStudent

    public Response<string> RestoreStudent(int id)
    {
        var res = context.Students.FirstOrDefault(x => x.Id == id && x.IsDeleted == true);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found"); 
        
        res.IsDeleted = false;
        var result = context.SaveChanges();
        return result > 0
            ? new Response<string>(HttpStatusCode.NoContent, "Student restored")
            : new Response<string>(HttpStatusCode.BadRequest, "no updated student");
    }

    #endregion
}