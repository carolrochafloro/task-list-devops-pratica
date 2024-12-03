using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace task_list_devops_pratica.Models;

public class AppUser
{
    [Key]
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }

    public AppUser()
    {
        Id = Guid.NewGuid();
    }

}
