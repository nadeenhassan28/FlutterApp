using Test.DTO;
using Test.Mappers;
using Test.Models;
using Test.Repositories.Abstract;
using Test.Services.Abstract;

namespace Test.Services.Implementation;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly string jwtSecret;
    public TaskService(ITaskRepository taskRepository, IConfiguration configuration)
    {
        _taskRepository = taskRepository;
        jwtSecret = configuration["JWT:Secret"]!;
    }

    public async Task<Response> GetTasks(int userId)
    {
        var tasks = await _taskRepository.GetAllForUserAsync(userId);
        if (tasks == null || !tasks.Any())
            return new Response(false, "No tasks found");

        var dtos = tasks.Select(TaskMapper.ToReadDTO).ToList();
        return new Response(true, "Tasks retrieved successfully", dtos);
    }

    public async Task<Response> GetTaskById(int id, int userId)
    {
        var task = await _taskRepository.GetByIdAsync(id, userId);
        if (task == null)
            return new Response(false, "Task not found");

        return new Response(true, "Task retrieved successfully", TaskMapper.ToReadDTO(task));
    }

    public async Task<Response> AddTask(TaskWriteDTO dto, int userId)
    {   
        var task = TaskMapper.ToUserTask(dto, userId);
        await _taskRepository.AddAsync(task);
        return new Response(true, "Task added successfully", TaskMapper.ToReadDTO(task));
    }

    public async Task<Response> UpdateTask(int id, TaskWriteDTO taskWriteDTO, int userId)
    {
        var task = await _taskRepository.GetByIdAsync(id, userId);
        if (task == null)
            return new Response(false, "Task not found");

        task.Name = taskWriteDTO.Name;
        task.Description = taskWriteDTO.Description;
        task.Startdate = taskWriteDTO.Startdate;
        task.Enddate = taskWriteDTO.Enddate;
        task.Status = taskWriteDTO.Status;

        await _taskRepository.UpdateAsync(task);

        return new Response(true, "Task updated successfully", TaskMapper.ToReadDTO(task));
    }


    public async Task<Response> DeleteTask(int id, int userId)
    {
        var task = await _taskRepository.GetByIdAsync(id, userId);
        if (task == null)
            return new Response(false, "Task not found");
        await _taskRepository.DeleteAsync(task);
        return new Response(true, "Task deleted successfully");
    }
}
