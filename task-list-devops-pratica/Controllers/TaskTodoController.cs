using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using task_list_devops_pratica.Context.Data;
using task_list_devops_pratica.Models.DTO;
using task_list_devops_pratica.Models.Entities;
using task_list_devops_pratica.Models.Interfaces;

namespace task_list_devops_pratica.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskTodoController : ControllerBase
{
    private readonly ITaskToDoData _taskToDoData;

    public TaskTodoController(ITaskToDoData taskToDoData)
    {
        _taskToDoData = taskToDoData;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(NewTaskDTO task)
    {
        if (task == null)
        {
            return BadRequest("Os dados devem ser informados");
        }

        var newTask = new TaskToDo
        {
            Name = task.Name,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            Deadline = task.Deadline,
        };

        await _taskToDoData.CreateTaskAsync(newTask);

        return Ok(newTask);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(bool isCompleted)
    {
        var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        Guid.TryParse(userIdString, out Guid userId);

        await _taskToDoData.UpdateTaskAsync(userId, isCompleted);

        return Ok("Tarefa atualizada.");
    }

    [HttpGet]
    public List<TaskToDo> GetTasks()
    {
        var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        Guid.TryParse(userIdString, out Guid userId);

        var taskList = _taskToDoData.ListTasks(userId);

        return taskList;
    }
}
