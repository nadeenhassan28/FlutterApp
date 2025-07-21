using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Test.Models;

namespace WebApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string username { get; set; } = string.Empty; 

        public required string passwordHash { get; set; }
        [Required]
        public string firstname { get; set; } = string.Empty;

        public string ImgUrl { get; set; } = string.Empty;

        public string? lastname { get; set; }

        [Required]
        public string address { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; } = Role.User;

        public ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();

    }
}