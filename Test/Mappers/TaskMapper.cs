using Test.DTO;
using Test.Models;

namespace Test.Mappers;

public static class TaskMapper
{
    public static UserTask ToUserTask(this TaskWriteDTO taskWriteDTO,int userId)
    {
        return new UserTask
        { Name = taskWriteDTO.Name,
          Description = taskWriteDTO.Description,
          Status = taskWriteDTO.Status,
          Startdate = taskWriteDTO.Startdate,
          Enddate = taskWriteDTO.Enddate,
          UserId = userId
        };


}
    public static TaskReadDTO ToReadDTO(UserTask task)
    {
        return new TaskReadDTO
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Status = task.Status,
            Startdate = task.Startdate,
            Enddate = task.Enddate
        };
    }
}
