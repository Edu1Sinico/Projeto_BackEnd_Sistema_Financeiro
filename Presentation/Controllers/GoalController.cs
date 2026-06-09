using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.UseCases.GoalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("goal")]
public class GoalController(createGoal createGoal, getGoal getGoal, getGoals getGoals, updateGoal updateGoal, deleteGoal deleteGoal) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] GoalCreateDTO dto)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await createGoal.create(dto, userId.Value), this);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await getGoal.getOne(id, userId.Value), this);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> List([FromQuery] int userId,
        [FromQuery][Range(1,int.MaxValue)][DefaultValue(1)] int page, 
        [FromQuery][Range(1,100)][DefaultValue(10)] int quantity )
    {
        var authenticatedUserId = CurrentUser.GetId(this);
        if (authenticatedUserId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await getGoals.getMany(userId, authenticatedUserId.Value,page,quantity), this);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GoalUpdateDTO dto)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await updateGoal.update(id, dto, userId.Value), this);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await deleteGoal.delete(id, userId.Value), this);
    }
}
