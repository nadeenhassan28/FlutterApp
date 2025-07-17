using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }

        [Required]
        public string username { get; set; } = string.Empty; 

        public string passwordHash { get; set; }
        [Required]
        public string firstname { get; set; } = string.Empty;

        public string? lastname { get; set; }


        [Required]
        public string address { get; set; } = string.Empty;

    }
}