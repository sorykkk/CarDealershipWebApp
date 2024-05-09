using System.Security.Cryptography;
using System.Text;

namespace CarDealersWebApp.Services;

public static class PasswordHelper
{
    public static string HashPassword(string rawPassword)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
        var hashedPassword = BitConverter.ToString(bytes).Replace("-", "").ToLower();

        return hashedPassword;
    }
}
