using Application.DTOs;
using Application.UseCases.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("user")]
[Produces("application/json")]
public class UserController(
    createUser createUser,
    getUser getUser,
    updateUser updateUser,
    deleteUser deleteUser) : ControllerBase
{
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post([FromBody] UserCreateDTO dto)
    {
        return HttpResponseMapper.createResponse(await createUser.create(dto), this);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return HttpResponseMapper.createResponse(await getUser.getOne(id), this);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserUpdateDTO dto)
    {
        return HttpResponseMapper.createResponse(await updateUser.update(id, dto), this);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HttpResponseMapper.createResponse(await deleteUser.delete(id), this);
    }
}