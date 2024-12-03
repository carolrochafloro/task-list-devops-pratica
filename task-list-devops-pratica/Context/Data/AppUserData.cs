using task_list_devops_pratica.Models;

namespace task_list_devops_pratica.Context.Data;

public class AppUserData
{
    private readonly TaskContext _context;

    public AppUserData(TaskContext context)
    {
        _context = context;
    }

    public async Task<string> CreateUserAsync(AppUser user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return "Usuário cadastrado com sucesso.";
    }

    public AppUser? GetUser(string email)
    {
        var user = _context.Set<AppUser>().Where(u => u.Email == email).FirstOrDefault();

        return user;
    }
}
