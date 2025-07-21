using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Test.DTO;
using Test.Models;
using Test.Repositories.Abstract;
using Test.Services.Abstract;

namespace Test.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ApiDbContext _context;
    private readonly ITaskService _taskService;
    private readonly ITaskRepository _taskRepository;

    public TaskController(ApiDbContext context, ITaskService taskService, ITaskRepository taskRepository)
    {
        _context = context;
        _taskService = taskService;
        _taskRepository = taskRepository;
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("userId") ?? User.FindFirst("sub");
        if (userIdClaim == null) throw new UnauthorizedAccessException("User ID claim not found.");
        return int.Parse(userIdClaim.Value);
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var userId = GetCurrentUserId();
        var tasks = await _taskService.GetTasks(userId);
        if (!tasks.Success)
        {
            return NotFound(tasks);
        }
        return Ok(tasks);
    }

    [Authorize(Roles = "User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var userId = GetCurrentUserId();
        var task = await _taskService.GetTaskById(id, userId);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskWriteDTO dto)
    {
        var userId = GetCurrentUserId();
        var task = await _taskService.AddTask(dto, userId);
        if (!task.Success)
        {
            return BadRequest(task);
        }
        return Ok(task);
    }
    [Authorize(Roles = "User")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id,TaskWriteDTO dto)
    {
        var userId = GetCurrentUserId();
        var updated = await _taskService.UpdateTask(id, dto, userId);
        if (!updated.Success)
        {
            return NotFound(updated);
        }
        return Ok(updated);
    }
    [Authorize(Roles = "User")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var userId = GetCurrentUserId();
        var deleted = await _taskService.DeleteTask(id, userId);
        if (!deleted.Success)
        {
            return NotFound(deleted);
        }
        return Ok(deleted);
    }
}
