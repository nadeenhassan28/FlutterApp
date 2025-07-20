using WebApi.Models;

namespace Test.Repositories.Abstract;

public interface IUserRepository
{
    Task AddUser(User user);
    Task DeleteUser(int id);
    Task<User?> GetUserById(int id);
    Task<IEnumerable<User>> GetUsers();
    Task UpdateUser(User user);
    Task<User?> GetUserByUsername(string username);    
}
