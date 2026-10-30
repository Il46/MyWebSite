using System.Security.Cryptography;
using System.Text;

namespace MyWebSite.Models;

public class UserAccount
{
    private readonly string login;
    private readonly byte[] salt = RandomNumberGenerator.GetBytes(16);
    private readonly byte[] passwordHash;

    public bool IsLoggedIn { get; private set; }

    public UserAccount(string login, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(login);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        this.login = login;
        passwordHash = HashPassword(password);
    }

    public bool Login(string inputLogin, string inputPassword)
    {
        if (string.IsNullOrEmpty(inputLogin) || string.IsNullOrEmpty(inputPassword))
        {
            return false;
        }

        var loginMatches = CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(login),
            Encoding.UTF8.GetBytes(inputLogin));
        var passwordMatches = CryptographicOperations.FixedTimeEquals(passwordHash, HashPassword(inputPassword));
        IsLoggedIn = loginMatches && passwordMatches;
        return IsLoggedIn;
    }

    public void Logout() => IsLoggedIn = false;

    public string GetAccountInfo() => $"Логин: {login}, Статус: {(IsLoggedIn ? "В сети" : "Офлайн")}";

    private byte[] HashPassword(string password) =>
        Rfc2898DeriveBytes.Pbkdf2(password, salt, 210_000, HashAlgorithmName.SHA256, 32);
}
