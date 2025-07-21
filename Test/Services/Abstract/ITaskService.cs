using Test.DTO;
using Test.Models;

namespace Test.Services.Abstract;


public interface ITaskService
{
    Task<Response> GetTasks(int userId);
    Task<Response> GetTaskById(int id, int userId);
    Task<Response> AddTask(TaskWriteDTO taskWriteDTO, int userId);
    Task<Response> UpdateTask(int id, TaskWriteDTO taskWriteDTO, int userId);
    Task<Response> DeleteTask(int id, int userId);
}
