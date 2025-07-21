using System.Text.Json.Serialization;
using Test.Models;

namespace Test.DTO;

public class TaskReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; set; }
    public DateTime Startdate { get; set; }
    public DateTime Enddate { get; set; }
}
