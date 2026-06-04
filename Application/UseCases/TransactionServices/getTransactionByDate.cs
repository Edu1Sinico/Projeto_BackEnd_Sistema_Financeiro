using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

public class getTransactionByDate(ITransactionRepository repository)
{
    public async Task<Result<TransactionListDTO>> getByDate(int userId,DateOnly date)
    {
        var transactions = await repository.GetTransactionsByDateAsync(userId,date);
        return Result<TransactionListDTO>.Success(new TransactionListDTO(transactions),200);
    }
}