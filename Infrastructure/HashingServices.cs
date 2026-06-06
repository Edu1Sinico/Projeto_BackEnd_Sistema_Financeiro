using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class HashingServices(IOptions<SecuritySettings> options)
{
    public string hashText(string text)
    {
        
        var settings = options.Value;
                
        var encodedText = Encoding.ASCII.GetBytes(text);
        
        var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(settings.secretSalt));
                    
        var hashedText = hmac.ComputeHash(encodedText);
        
        return BitConverter.ToString(hashedText);
        
    }
}