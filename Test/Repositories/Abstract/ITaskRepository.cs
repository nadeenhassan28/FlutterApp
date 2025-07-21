using Test.Models;

namespace Test.Repositories.Abstract;

public interface ITaskRepository
{
    Task<UserTask?> GetByIdAsync(int id, int userId);

    Task<IEnumerable<UserTask>> GetAllForUserAsync(int userId);
    Task AddAsync(UserTask task);
    Task UpdateAsync(UserTask task);
    Task DeleteAsync(UserTask task);
}
