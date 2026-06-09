using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

public class getTransactionsByTimePeriod(ITransactionRepository repository)
{
    public async Task<Result<TransactionListDTO>> getByPeriod(int userId, DateOnly startDate, DateOnly endDate,int page, int quantity)
        => Result<TransactionListDTO>.Success(new TransactionListDTO(await repository.GetTransactionsByTimePeriodAsync(userId, startDate, endDate, page,  quantity),page, quantity), 200);
}