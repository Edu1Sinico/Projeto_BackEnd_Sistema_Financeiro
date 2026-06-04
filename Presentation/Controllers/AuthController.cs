using System.Data.SqlTypes;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure;

[ApiController]
[Route("auth")]
public class AuthController(AuthenticationService authenticationService)
{
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> login([FromBody]UserCredentialsDTO userCredentialsDto)
    {
        return await authenticationService.validate(userCredentialsDto);
    }
}