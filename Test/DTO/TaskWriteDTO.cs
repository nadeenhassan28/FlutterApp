using Test.Models;

namespace Test.DTO;

public class TaskWriteDTO
{
    public string Description { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }
}
