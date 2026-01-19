using Domain.DTOs.Student;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService service) : ControllerBase
{
    [HttpGet("all")]
    public IActionResult GetAllStudents()
    {
        var res = service.GetStudents();
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("id")]
    public IActionResult GetStudentById(int id)
    {
        var res = service.GetStudentById(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("create")]
    public IActionResult CreatedStudent(CreatedStudent dto)
    {
        var res = service.CreatedStudent(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut("update")]
    public IActionResult UpdateStudent(int id, UpdateStudent dto)
    {
        var res = service.UpdateStudent(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteStudent(int id)
    {
        var res = service.DeleteStudent(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("admin/get-deleted-students")]
    public IActionResult AdminGetStudents()
    {
        var res = service.GetStudentsDeleted();
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut("{id}/restore")]
    public IActionResult RestoreStudent(int id)
    {
        var res = service.RestoreStudent(id);
        return StatusCode(res.StatusCode, res);
    }
}