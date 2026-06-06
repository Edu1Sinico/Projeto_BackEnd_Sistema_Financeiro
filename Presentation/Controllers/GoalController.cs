using Application.DTOs;
using Application.UseCases.GoalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("goal")]
[Produces("application/json")]
public class GoalController(
    createGoal createGoal,
    getGoal getGoal,
    getGoals getGoals,
    updateGoal updateGoal,
    deleteGoal deleteGoal) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] GoalCreateDTO dto)
    {
        return HttpResponseMapper.createResponse(await createGoal.create(dto), this);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return HttpResponseMapper.createResponse(await getGoal.getOne(id), this);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> List([FromQuery] int userId)
    {
        return HttpResponseMapper.createResponse(await getGoals.getMany(userId), this);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GoalUpdateDTO dto)
    {
        return HttpResponseMapper.createResponse(await updateGoal.update(id, dto), this);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HttpResponseMapper.createResponse(await deleteGoal.delete(id), this);
    }
}