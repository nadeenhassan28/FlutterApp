using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApi.Models;

namespace Test.Models;

public class UserTask
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required Status Status { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public int userId { get; set; }
    [ForeignKey("UserId")]
    [JsonIgnore]

    public User User { get; set; } = null!;


}
