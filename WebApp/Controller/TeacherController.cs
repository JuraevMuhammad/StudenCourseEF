using Domain.DTOs.Teacher;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("api/[controller]/teacher")]
public class TeacherController(ITeacherService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetTeachers()
    {
        var res = service.GetTeachers();
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("{id}")]
    public IActionResult GetTeacher(int id)
    {
        var res = service.GetTeacher(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost]
    public IActionResult CreatedTeacher(CreatedTeacher dto)
    {
        var res = service.CreatedTeacher(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut]
    public IActionResult UpdateTeacher(int id,UpdateTeacher dto)
    {
        var res = service.UpdateTeacher(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTeacher(int id)
    {
        var res = service.DeleteTeacher(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("with-courses")]
    public IActionResult GetTeachersWithCourses()
    {
        var res = service.GetTeachersWithCourses();
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("search-name")]
    public IActionResult GetTeacherSearchName(string searchName)
    {
        var res = service.GetTeachersSearchName(searchName);
        return StatusCode(res.StatusCode, res);
    }
}