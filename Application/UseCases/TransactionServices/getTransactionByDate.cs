using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

public class getTransactionByDate(ITransactionRepository repository)
{
    public async Task<Result<TransactionListDTO>> getByDate(int userId, DateOnly date,int page, int quantity) 
        => Result<TransactionListDTO>.Success(new TransactionListDTO(await repository.GetTransactionsByDateAsync(userId, date, page,  quantity),page, quantity), 200);
}