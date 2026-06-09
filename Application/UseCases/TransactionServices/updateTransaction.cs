using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

public class updateTransaction(ITransactionRepository repository, IAccountRepository accountRepository, ICategoryRepository categoryRepository)
{
    public async Task<Result<Transaction>> update(int id, TransactionUpdateDTO dto, int userId)
    {
        var transaction = await repository.GetTransactionAsync(id);
        if (transaction == null) return Result<Transaction>.Failure("Transacao nao encontrada", 404);
        if (dto.amount <= 0) return Result<Transaction>.Failure("Valor da transacao deve ser maior que zero", 400);

        var oldAccount = await accountRepository.GetAccount(transaction.accountId);
        var newAccount = await accountRepository.GetAccount(dto.accountId);
        var category = await categoryRepository.GetCategoryAsync(dto.categoryId);
        if (oldAccount == null || newAccount == null) return Result<Transaction>.Failure("Conta nao encontrada", 404);
        if (category == null) return Result<Transaction>.Failure("Categoria nao encontrada", 404);
        if (oldAccount.userId != userId || newAccount.userId != userId || category.userId != userId) return Result<Transaction>.Failure("Usuario nao pode alterar transacao de outro usuario", 403);
        if (category.type != dto.type) return Result<Transaction>.Failure("Categoria nao corresponde ao tipo da transacao", 400);

        oldAccount.balance = RevertImpact(oldAccount.balance, transaction.amount, transaction.type);
        var targetBalance = oldAccount.id == newAccount.id ? oldAccount.balance : newAccount.balance;
        targetBalance = ApplyImpact(targetBalance, dto.amount, dto.type);
        if (targetBalance < 0) return Result<Transaction>.Failure("Saldo insuficiente para esta despesa", 400);

        if (oldAccount.id == newAccount.id)
        {
            oldAccount.balance = targetBalance;
            await accountRepository.UpdateAccountAsync(oldAccount);
        }
        else
        {
            newAccount.balance = targetBalance;
            await accountRepository.UpdateAccountAsync(oldAccount);
            await accountRepository.UpdateAccountAsync(newAccount);
        }

        transaction.description = dto.description;
        transaction.amount = dto.amount;
        transaction.type = dto.type;
        transaction.accountId = dto.accountId;
        transaction.categoryId = dto.categoryId;
        await repository.UpdateTransactionAsync(transaction);
        return Result<Transaction>.Success(transaction, 200);
    }

    private static decimal ApplyImpact(decimal balance, decimal amount, TransactionType type) => type == TransactionType.Receita ? balance + amount : balance - amount;
    private static decimal RevertImpact(decimal balance, decimal amount, TransactionType type) => type == TransactionType.Receita ? balance - amount : balance + amount;
}