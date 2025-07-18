﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Models;
using Test.Repositories.Abstract;
using WebApi.Models;

namespace Test.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    private readonly ApiDbContext _db;
    public UserRepository(ApiDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.userId == id);
        return user;
    }

    public async Task UpdateUser(User user)
    {
        _db.Users.Update(user);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

    }

    public async Task AddUser(User user,string password)
    {
        user.passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

    }

    public async Task<User?> Login(string username,string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.passwordHash))
        {
            return null; 
        }
        return user;                                                               
    }
        
}                           
