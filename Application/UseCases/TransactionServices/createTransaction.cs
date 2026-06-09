
using Application.DTOs;
using Domain;
using Domain.Models;

public class createTransaction(ITransactionRepository repository, IAccountRepository accountRepository, ICategoryRepository categoryRepository)
{
    public async Task<Result<Transaction>> create(TransactionCreateDTO dto, int userId)
    {
        var validation = await Validate(dto.accountId, dto.categoryId, dto.type, userId);
        if (!validation.isSuccess) return Result<Transaction>.Failure(validation.error!, validation.code);
        if (dto.amount <= 0) return Result<Transaction>.Failure("Valor da transacao deve ser maior que zero", 400);

        var account = validation.value!.account;
        var newBalance = ApplyImpact(account.balance, dto.amount, dto.type);
        if (newBalance < 0) return Result<Transaction>.Failure("Saldo insuficiente para esta despesa", 400);

        var transaction = new Transaction(dto.description, dto.amount, dto.type, DateOnly.FromDateTime(DateTime.UtcNow), dto.accountId, dto.categoryId);
        account.balance = newBalance;
        await accountRepository.UpdateAccountAsync(account);
        await repository.CreateTransactionAsync(transaction);
        return Result<Transaction>.Success(transaction, 201);
    }

    private async Task<Result<(Account account, Category category)>> Validate(int accountId, int categoryId, TransactionType type, int userId)
    {
        var account = await accountRepository.GetAccount(accountId);
        var category = await categoryRepository.GetCategoryAsync(categoryId);
        if (account == null) return Result<(Account, Category)>.Failure("Conta nao encontrada", 404);
        if (category == null) return Result<(Account, Category)>.Failure("Categoria nao encontrada", 404);
        if (account.userId != userId) return Result<(Account, Category)>.Failure("Usuario nao pode usar conta de outro usuario", 403);
        if (category.userId != userId) return Result<(Account, Category)>.Failure("Usuario nao pode usar categoria de outro usuario", 403);
        if (category.type != type) return Result<(Account, Category)>.Failure("Categoria nao corresponde ao tipo da transacao", 400);
        return Result<(Account, Category)>.Success((account, category), 200);
    }

    private static decimal ApplyImpact(decimal balance, decimal amount, TransactionType type) => type == TransactionType.Receita ? balance + amount : balance - amount;
}
