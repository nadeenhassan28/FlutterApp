namespace Test.DTO;

public record UserWriteDTO
{
    public string Firstname { get; set; } = string.Empty;
    public string? Lastname { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IFormFile? Photo { get; set; }

}

