using Test.DTO;
using Test.Models;

namespace Test.Mappers;

public static class TaskMapper
{
    public static UserTask ToUserTask(this TaskWriteDTO taskWriteDTO)
    {
        return new UserTask
        { Name = taskWriteDTO.Name,
          Description = taskWriteDTO.Description,
          Status = taskWriteDTO.Status,
          Startdate = taskWriteDTO.Startdate,
          Enddate = taskWriteDTO.Enddate,
        };


}
}
