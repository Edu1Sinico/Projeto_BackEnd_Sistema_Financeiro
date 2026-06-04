using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

public class getTransactionsByTimePeriod(ITransactionRepository repository)
{
    public async Task<Result<TransactionListDTO>> getByPeriod(int userId,DateOnly startDate,DateOnly endDate)
    {
        var transactions = 
            await repository.GetTransactionsByTimePeriodAsync(userId,startDate,endDate);
        return Result<TransactionListDTO>.Success(new TransactionListDTO(transactions),200);

    }
}