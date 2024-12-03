using task_list_devops_pratica.Models.Entities;

namespace task_list_devops_pratica.Models.Interfaces;

public interface ITaskToDoData
{
    Task<TaskToDo> CreateTaskAsync(TaskToDo newTask);
    Task<TaskToDo?> UpdateTaskAsync(Guid id, bool isCompleted);
    List<TaskToDo> ListTasks(Guid id);
}
