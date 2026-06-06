using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

public class createTransaction(ITransactionRepository repository)
{
    public async Task<Result<Transaction>> create(TransactionCreateDTO dto)
    {
        var transaction = new Transaction(
            dto.description,
            dto.amount,
            dto.type,
            DateOnly.FromDateTime(DateTime.UtcNow),
            dto.accountId,
            dto.category);

        await repository.CreateTransactionAsync(transaction);
        return Result<Transaction>.Success(transaction, 201);
    }
}