using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class HashingServices(IOptions<SecuritySettings> options) : IHashingService
{
    public string HashText(string text)
    {
        var settings = options.Value;
        using var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(settings.secretSalt));
        return BitConverter.ToString(hmac.ComputeHash(Encoding.ASCII.GetBytes(text)));
    }

    public string hashText(string text) => HashText(text);
}
