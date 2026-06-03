using Application.DTOs;
using Application.UseCases.Account;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("account")]
[Produces("application/json")]
public class AccountController(
    createAccount createAccount,
    updateAccount updateAccount,
    deleteAccount deleteAccount) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] AccountCreateDTO dto)
    {
        return HttpResponseMapper.createResponse(await createAccount.create(dto), this);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AccountUpdateDTO dto)
    {
        return HttpResponseMapper.createResponse(await updateAccount.update(id, dto), this);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HttpResponseMapper.createResponse(await deleteAccount.delete(id), this);
    }
}