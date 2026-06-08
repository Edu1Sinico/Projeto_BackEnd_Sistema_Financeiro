using Application.DTOs;
using Application.UseCases.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("user")]
public class UserController(createUser createUser, getUser getUser, updateUser updateUser, deleteUser deleteUser) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] UserCreateDTO dto) => HttpResponseMapper.createResponse(await createUser.create(dto), this);

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        if (id != userId.Value) return Forbid();
        return HttpResponseMapper.createResponse(await getUser.getOne(id), this);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserUpdateDTO dto)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        if (id != userId.Value) return Forbid();
        return HttpResponseMapper.createResponse(await updateUser.update(id, dto), this);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        if (id != userId.Value) return Forbid();
        return HttpResponseMapper.createResponse(await deleteUser.delete(id), this);
    }
}
