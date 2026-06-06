
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public class TokenService(JwtSecurityTokenHandler handler,Context context, HashingServices hashServices, IOptions<SecuritySettings> options)


{
    public Result<string> generateToken(User user)
    {
        var securitySettings = options.Value;
        
        var keyBytes = Encoding.ASCII.GetBytes(securitySettings.jwtSecretKey);

        var credentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256Signature);

     
     
     
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = credentials,
            
        
        
        };


        var token = handler.CreateToken(tokenDescriptor);

        return Result<string>.Success(handler.WriteToken(token),200);
    }
    
    


    

    public static ClaimsIdentity GenerateClaims(User user)
    {
        var claimsIdentity = new ClaimsIdentity();
        
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.id.ToString())); // o mais importante
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.email));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.name));
        claimsIdentity.AddClaim(new Claim("createdAt", user.creationDate.ToString()));
        return claimsIdentity;

    }


}