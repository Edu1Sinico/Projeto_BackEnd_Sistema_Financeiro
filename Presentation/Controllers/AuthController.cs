using System.Data.SqlTypes;
using Application.DTOs;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation;

namespace Presentation.Controllers;

[ApiController]
[Route("auth")]
[Produces("application/json")]
public class AuthController(AuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> login([FromBody]UserCredentialsDTO userCredentialsDto)
    {
        return HttpResponseMapper.createResponse(await authenticationService.validate(userCredentialsDto),this);
    }
}