using Test.DTO;
using WebApi.Models;

namespace Test.Mappers;

public static class UserMapper
{
    public static User ToUser(this UserWriteDTO userWriteDTO)
    {
        return new User
        {
            firstname = userWriteDTO.Firstname,
            lastname = userWriteDTO.Lastname,
            address = userWriteDTO.Address,
            username = userWriteDTO.Username,
            passwordHash  = userWriteDTO.Password
        };
    }
}
