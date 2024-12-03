using System.ComponentModel.DataAnnotations;

namespace task_list_devops_pratica.Models.DTO;

public class LoginDTO
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
