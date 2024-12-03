using task_list_devops_pratica.Models.Entities;
using task_list_devops_pratica.Models.Interfaces;

namespace task_list_devops_pratica.Context.Data;

public class TaskToDoData : ITaskToDoData
{
    private readonly TaskContext _context;

    public TaskToDoData(TaskContext context)
    {
        _context = context;
    }

    public async Task<TaskToDo> CreateTaskAsync(TaskToDo newTask)
    {
        await _context.AddAsync(newTask);
        await _context.SaveChangesAsync();

        return newTask;
    }

    public async Task<TaskToDo?> UpdateTaskAsync(Guid id, bool isCompleted)
    {
        var task = _context.Set<TaskToDo>().Where(t => t.Id == id).FirstOrDefault();

        if (task == null)
        {
            return null;
        }

        task.IsCompleted = isCompleted;
        await _context.SaveChangesAsync();

        return task;
    }

    public List<TaskToDo> ListTasks(Guid id)
    {

        var taskList = _context.Set<TaskToDo>().Where(u => u.UserId == id).ToList();

        return taskList;

    }
}
