using Application.DTOs;
using Application.UseCases.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("transaction")]
[Produces("application/json")]
public class TransactionController(
    createTransaction createTransaction,
    getTransaction getTransaction,
    getTransactionByDate getTransactionByDate,
    getTransactionsByTimePeriod getTransactionsByTimePeriod) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] TransactionCreateDTO dto)
    {
        return HttpResponseMapper.createResponse(await createTransaction.create(dto), this);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return HttpResponseMapper.createResponse(await getTransaction.getOne(id), this);
    }

    [HttpGet("by-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDate([FromQuery] int userId, [FromQuery] DateOnly date)
    {
        return HttpResponseMapper.createResponse(await getTransactionByDate.getOne(userId, date), this);
    }

    [HttpGet("by-period")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByPeriod(
        [FromQuery] int userId,
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate)
    {
        return HttpResponseMapper.createResponse(await getTransactionsByTimePeriod.getOne(userId, startDate, endDate), this);
    }
}