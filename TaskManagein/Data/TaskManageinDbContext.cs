using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TaskManagein.Data.Map;
using TaskManagein.Models;

namespace TaskManagein.Data
{
    public class TaskManageinDbContext : DbContext
    {
        public TaskManageinDbContext(DbContextOptions<TaskManageinDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TaskMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
