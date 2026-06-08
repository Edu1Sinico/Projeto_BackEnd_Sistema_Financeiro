using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.TransactionServices;

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

public class getTransactionByDate(ITransactionRepository repository)
{
    public async Task<Result<List<Transaction>>> getByDate(int userId, DateOnly date) => Result<List<Transaction>>.Success(await repository.GetTransactionsByDateAsync(userId, date), 200);
}

public class getTransactionsByTimePeriod(ITransactionRepository repository)
{
    public async Task<Result<List<Transaction>>> getByPeriod(int userId, DateOnly startDate, DateOnly endDate) => Result<List<Transaction>>.Success(await repository.GetTransactionsByTimePeriodAsync(userId, startDate, endDate), 200);
}
