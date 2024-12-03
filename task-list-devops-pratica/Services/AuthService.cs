using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using task_list_devops_pratica.Context;
using task_list_devops_pratica.Context.Data;

namespace task_list_devops_pratica.Services;

public class AuthService
{
    private readonly AppUserData _userData;

    public AuthService(AppUserData userData)
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

    public bool Authenticate(string email, string password)
    {
        var user = _userData.GetUser(email);

        if (user is null)
        {
            return false;
        }

        if (!IsValidPassword(password, user.Salt, user.Password))
        {
            return false;
        }

        return true;
    }
}
