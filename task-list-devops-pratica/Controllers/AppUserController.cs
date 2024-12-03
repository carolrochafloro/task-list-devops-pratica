using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_list_devops_pratica.Context.Data;
using task_list_devops_pratica.Models.DTO;
using task_list_devops_pratica.Models.Entities;
using task_list_devops_pratica.Models.Interfaces;
using task_list_devops_pratica.Services;

namespace task_list_devops_pratica.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppUserController : ControllerBase
{
    private readonly IAppUserData _userData;
    private readonly IAuthService _authService;

    public AppUserController(IAppUserData userData,
                             IAuthService authService)
    {
        _userData = userData;
        _authService = authService;
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> CreateUser(RegisterDTO user)
    {
        if (user == null)
        {
            return BadRequest("Os dados devem ser informados");
        }

        var checkUser = _userData.GetUser(user.Email);

        if (checkUser is not null)
        {
            return BadRequest("E-mail em uso");
        }

        var salt = _authService.SaltGenerator();
        var hashedPassword = _authService.HashPassword(user.Password, salt);

        var newUser = new AppUser
        {
            Name = user.Name,
            Email = user.Email,
            Password = hashedPassword,
            Salt = salt
        };

        await _userData.CreateUserAsync(newUser);

        var jwt = _authService.GenerateJwt(newUser);

        return Ok(new JwtDTO
        {
            JWT = jwt,
            Message = $"{user.Name} criado com sucesso e autenticado."
        });

    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO login)
    {
        var user = _userData.GetUser(login.Email);

        var validateUser = _authService.Authenticate(login);

        if (!validateUser)
        {
            return Unauthorized("E-mail ou senha inválidos.");
        }

        var jwt = _authService.GenerateJwt(user);

        return Ok(new JwtDTO
        {
            JWT = jwt,
            Message = "Usuário autenticado com sucesso."
        });
    }
}
