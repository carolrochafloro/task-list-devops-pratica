using Microsoft.EntityFrameworkCore;
using task_list_devops_pratica.Models.Entities;

namespace task_list_devops_pratica.Context;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options)
    {

    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<TaskToDo> Tasks { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
