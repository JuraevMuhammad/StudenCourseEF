using Domain.DTOs.Course;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var res = service.GetAllCourse();
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("id")]
    public IActionResult GetById(int id)
    {
        var res = service.GetCourse(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("with-teacher")]
    public IActionResult GetWithTeacher()
    {
        var res = service.GetAllCoursesWithTeacher();
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost]
    public IActionResult CreatedCourse(CreatedCourse dto)
    {
        var res = service.CreatedCourse(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut]
    public IActionResult UpdateCourse(int id, UpdateCourse dto)
    {
        var res = service.UpdateCourse(id, dto);
        return  StatusCode(res.StatusCode, res);
    }

    [HttpDelete]
    public IActionResult DeleteCourse(int id)
    {
        var res = service.DeleteCourse(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("filter")]
    public IActionResult GetCourseFilterPrice(decimal minPrice, decimal maxPrice)
    {
        var res = service.GetCoursesFilterPrice(minPrice, maxPrice);
        return StatusCode(res.StatusCode, res);
    }
}