﻿using Test.DTO;
using Test.Models;
using WebApi.Models;

namespace Test.Services.Abstract;

public interface IUserService
{
    Task<Response> AddUser(UserWriteDTO user);
    Task<Response> DeleteUser(int id);
    Task<Response> GetUserById(int id);
    Task<Response> GetUsers();
    Task<Response> UpdateUser(User user);
    Task<Response> Login(UserLoginDTO loginDto);
    Task<Response> UpdateUserImg(IFormFile img, int userId, string requestScheme, HostString requestHost);
}
