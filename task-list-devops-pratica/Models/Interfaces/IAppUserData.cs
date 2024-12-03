using task_list_devops_pratica.Models.Entities;

namespace task_list_devops_pratica.Models.Interfaces;

public interface IAppUserData
{
    Task<string> CreateUserAsync(AppUser user);
    AppUser? GetUser(string email);
}
