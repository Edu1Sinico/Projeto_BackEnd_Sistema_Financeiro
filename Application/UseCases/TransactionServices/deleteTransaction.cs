using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

public class deleteTransaction(ITransactionRepository repository, IAccountRepository accountRepository)
{
    public async Task<Result<Transaction>> delete(int id, int userId)
    {
        var transaction = await repository.GetTransactionAsync(id);
        if (transaction == null) return Result<Transaction>.Failure("Transacao nao encontrada", 404);
        var account = await accountRepository.GetAccount(transaction.accountId);
        if (account == null) return Result<Transaction>.Failure("Conta nao encontrada", 404);
        if (account.userId != userId) return Result<Transaction>.Failure("Usuario nao pode excluir transacao de outro usuario", 403);

        account.balance = transaction.type == TransactionType.Receita ? account.balance - transaction.amount : account.balance + transaction.amount;
        await accountRepository.UpdateAccountAsync(account);
        await repository.DeleteTransactionAsync(transaction);
        return Result<Transaction>.NoContent();
    }
}