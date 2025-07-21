using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Test.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions option) : base(option)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

}
