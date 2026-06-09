using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.UseCases.TransactionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("transaction")]
[Produces("application/json")]
public class TransactionController(
    createTransaction createTransaction,
    getTransaction getTransaction,
    getTransactionByDate getTransactionByDate,
    getTransactionsByTimePeriod getTransactionsByTimePeriod,
    updateTransaction updateTransaction,
    deleteTransaction deleteTransaction) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] TransactionCreateDTO dto)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await createTransaction.create(dto, userId.Value), this);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await getTransaction.getOne(id, userId.Value), this);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TransactionUpdateDTO dto)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await updateTransaction.update(id, dto, userId.Value), this);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await deleteTransaction.delete(id, userId.Value), this);
    }

    [HttpGet("by-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetByDate(
        [FromQuery] int userId,
        [FromQuery] DateOnly date,
        [FromQuery][Range(1,int.MaxValue)][DefaultValue(1)] int page, 
        [FromQuery][Range(1,100)][DefaultValue(10)] int quantity)
    {
        var authenticatedUserId = CurrentUser.GetId(this);
        if (authenticatedUserId == null) return Unauthorized();
        if (userId != authenticatedUserId.Value) return Forbid();
        return HttpResponseMapper.createResponse(await getTransactionByDate.getByDate(userId, date,page,quantity), this);
    }

    [HttpGet("by-period")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> GetByPeriod(
        [FromQuery] int userId,
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        [FromQuery][Range(1,int.MaxValue)][DefaultValue(1)] int page, 
        [FromQuery][Range(1,100)][DefaultValue(10)] int quantity
        )
    {
        var authenticatedUserId = CurrentUser.GetId(this);
        if (authenticatedUserId == null) return Unauthorized();
        if (userId != authenticatedUserId.Value) return Forbid();
        return HttpResponseMapper.createResponse(await getTransactionsByTimePeriod.getByPeriod(userId, startDate, endDate,page,quantity), this);
    }
}