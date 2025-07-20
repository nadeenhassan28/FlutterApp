using Microsoft.AspNetCore.Mvc;
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
}
