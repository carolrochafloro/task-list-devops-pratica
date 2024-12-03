using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace task_list_devops_pratica.Models;

public class TaskToDo
{
    [Key]
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateOnly Deadline { get; set; }
    public Guid UserId { get; set; }

    [JsonIgnore]
    [ForeignKey("UserId")]
    public AppUser AppUser { get; set; }


    public TaskToDo()
    {
        Id = Guid.NewGuid();
    }
}
