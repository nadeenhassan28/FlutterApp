using Microsoft.EntityFrameworkCore;
using Test.Models;
using Test.Repositories.Abstract;

namespace Test.Repositories.Implementation;

public class TaskRepository : ITaskRepository
{
    private readonly ApiDbContext _db;
    public TaskRepository(ApiDbContext db)
    {
        _db = db;
    }

    public async Task<UserTask?> GetByIdAsync(int id, int userId)
    {
       var task = await _db.UserTasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        return task;
    }
    public async Task<IEnumerable<UserTask>> GetAllForUserAsync(int userId)
    {

       var task = await _db.UserTasks.Where(t => t.UserId == userId).ToListAsync();
       return task;
    }

    public async Task UpdateAsync(UserTask task)
    {
        _db.UserTasks.Update(task);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserTask task)
    {
        _db.UserTasks.Remove(task);
        await _db.SaveChangesAsync();
    }

    public async Task AddAsync(UserTask task)
    {
        await _db.UserTasks.AddAsync(task);
        await _db.SaveChangesAsync();
    }

}
