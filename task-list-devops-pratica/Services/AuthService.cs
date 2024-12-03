using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using task_list_devops_pratica.Context.Data;
using task_list_devops_pratica.Models.DTO;
using task_list_devops_pratica.Models.Entities;
using task_list_devops_pratica.Models.Interfaces;

namespace task_list_devops_pratica.Services;

public class AuthService : IAuthService
{
    private readonly IAppUserData _userData;

    public AuthService(IAppUserData userData)
    {
        _userData = userData;
    }

    public string SaltGenerator()
    {

        byte[] salt = new byte[16];

        RandomNumberGenerator.Fill(salt);

        return Convert.ToBase64String(salt);
    }

    public string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        using (var hmac = new HMACSHA256(saltBytes))
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = hmac.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hash);
        }
    }

    public bool IsValidPassword(string password, string salt, string hash)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        using (var hmac = new HMACSHA256(saltBytes))
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] computedHash = hmac.ComputeHash(passwordBytes);
            string computedHashString = Convert.ToBase64String(computedHash);

            return computedHashString.Equals(hash);
        }
    }

    public bool Authenticate(LoginDTO login)
    {
        var user = _userData.GetUser(login.Email);

        if (user is null)
        {
            return false;
        }

        if (!IsValidPassword(login.Password, user.Salt, user.Password))
        {
            return false;
        }

        return true;
    }

    public string GenerateJwt(AppUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hardCodedKeyForNow1234567890!_testeeeee"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
           issuer: "TaskTodo",
           audience: "TaskTodo",
           claims: claims,
           expires: DateTime.Now.AddMinutes(30),
           signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
