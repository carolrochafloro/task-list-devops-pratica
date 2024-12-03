using System.ComponentModel.DataAnnotations;

namespace task_list_devops_pratica.Models.DTO;

public class NewTaskDTO
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateOnly Deadline { get; set; }
}
