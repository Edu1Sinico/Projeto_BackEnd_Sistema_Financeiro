using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(AuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> login([FromBody] UserCredentialsDTO dto) =>
        HttpResponseMapper.createResponse(await authenticationService.validate(dto), this);
}
