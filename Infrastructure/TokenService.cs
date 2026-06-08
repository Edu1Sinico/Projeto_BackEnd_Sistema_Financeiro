using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public class TokenService(JwtSecurityTokenHandler handler, IOptions<SecuritySettings> options)
{
    public Result<string> generateToken(User user)
    {
        var keyBytes = Encoding.ASCII.GetBytes(options.Value.jwtSecretKey);
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };
        return Result<string>.Success(handler.WriteToken(handler.CreateToken(descriptor)), 200);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Email, user.email));
        claims.AddClaim(new Claim(ClaimTypes.Name, user.name));
        return claims;
    }
}
