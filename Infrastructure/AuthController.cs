using System.Data.SqlTypes;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure;

[ApiController]
[Route("auth")]
public class AuthController
{
    public string login(UserCredentialsDTO userCredentialsDto)
    {
        return AuthenticationService.validate(userCredentialsDto);
    }
}