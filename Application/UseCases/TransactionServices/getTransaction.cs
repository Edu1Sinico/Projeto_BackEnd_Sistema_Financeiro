using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

public class getTransaction(ITransactionRepository repository)
{
    public async Task<Result<Transaction>> getOne(int transactionId)
    {
        var transaction = await repository.GetTransactionAsync(transactionId);
        if (transaction == null) { return Result<Transaction>.Failure("Transação não encontrada", 404); }

        return Result<Transaction>.Success(transaction, 200);
    }
}