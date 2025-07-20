using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.DTO;
using Test.Mappers;
using Test.Models;
using Test.Repositories.Abstract;
using Test.Services.Abstract;
using WebApi.Models;
using Microsoft.AspNetCore.Identity;
namespace Test.Services.Implementation;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly string jwtSecret;
    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        jwtSecret = configuration["JWT:Secret"]!;
    }

    public async Task<Response> GetUsers()
    {
        var users =  await _userRepository.GetUsers();
        if (users == null)
            return new Response(false, "No users found");

        return new Response(true, "Users retrieved successfully", users);
    }

    public async Task<Response> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
            return new Response(false, "User not found");
        return new Response(true, "User retrieved successfully", user); 
    }

    public async Task<Response> AddUser(UserWriteDTO userWriteDTO)
    {
        var user = userWriteDTO.ToUser();
        user.passwordHash = BCrypt.Net.BCrypt.HashPassword(userWriteDTO.Password);
        await _userRepository.AddUser(user);
        var token = GenerateJWT(user);
        return new Response(true, "User added successfully", new { user, token });
    }
    public async Task<Response> Login(UserLoginDTO userloginDTO)
    {
        var user = await _userRepository.GetUserByUsername(userloginDTO.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(userloginDTO.Password, user.passwordHash))
            return new Response(false, "Invalid username or password");

        var token = GenerateJWT(user);
        return new Response(true, "Login successful", new { user, token });
    }
    public async Task<Response> UpdateUser(User user)
    {
        if (user == null)
            return new Response(false, "User cannot be null");
        await _userRepository.UpdateUser(user);
        return new Response(true, "User updated successfully", user);
    }

    public async Task<Response> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
            return new Response(false, "User not found");
        await _userRepository.DeleteUser(id);
        return new Response(true, "User deleted successfully");
    }

    private string GenerateJWT(User user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
        new Claim(ClaimTypes.Name, user.firstname),
        //new Claim(ClaimTypes.Role, user.Role.ToString())
    };

        var claimsIdentity = new ClaimsIdentity(claims);

        // make sure to change the secret here to match your jwt secret
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessTokenString = tokenHandler.WriteToken(token);

        return accessTokenString;
    }

}

