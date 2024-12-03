using task_list_devops_pratica.Models.DTO;
using task_list_devops_pratica.Models.Entities;

namespace task_list_devops_pratica.Models.Interfaces;

public interface IAuthService
{
    string SaltGenerator();
    string HashPassword(string password, string salt);
    bool IsValidPassword(string password, string salt, string hash);
    bool Authenticate(LoginDTO login);
    string GenerateJwt(AppUser user);
}
