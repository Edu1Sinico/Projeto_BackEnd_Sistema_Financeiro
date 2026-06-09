using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

public class getTransaction(ITransactionRepository repository, IAccountRepository accountRepository)
{
    public async Task<Result<Transaction>> getOne(int id, int userId)
    {
        var transaction = await repository.GetTransactionAsync(id);
        if (transaction == null) return Result<Transaction>.Failure("Transacao nao encontrada", 404);
        var account = await accountRepository.GetAccount(transaction.accountId);
        if (account == null) return Result<Transaction>.Failure("Conta nao encontrada", 404);
        if (account.userId != userId) return Result<Transaction>.Failure("Usuario nao pode acessar transacao de outro usuario", 403);
        return Result<Transaction>.Success(transaction, 200);
    }
}