using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

public class createAccount(IAccountRepository repository)
{
    public async Task<Result<Account>> create(AccountCreateDTO dto, int userId)
    {
        if (dto.userId != userId) return Result<Account>.Failure("Usuario nao pode criar conta para outro usuario", 403);
        if (string.IsNullOrWhiteSpace(dto.name)) return Result<Account>.Failure("Nome da conta nao pode ser vazio", 400);
        if (dto.balance < 0) return Result<Account>.Failure("Saldo nao pode ser negativo", 400);

        var account = new Account(dto.name.Trim(), dto.balance, dto.type, dto.userId);
        await repository.CreateAccountAsync(account);
        return Result<Account>.Success(account, 201);
    }
}

public class getAccount(IAccountRepository repository)
{
    public async Task<Result<Account>> getOne(int id, int userId)
    {
        var account = await repository.GetAccount(id);
        if (account == null) return Result<Account>.Failure("Conta nao encontrada", 404);
        if (account.userId != userId) return Result<Account>.Failure("Usuario nao pode acessar conta de outro usuario", 403);
        return Result<Account>.Success(account, 200);
    }
}

public class getAccounts(IAccountRepository repository)
{
    public async Task<Result<List<Account>>> getMany(int userId, int authenticatedUserId)
    {
        if (userId != authenticatedUserId) return Result<List<Account>>.Failure("Usuario nao pode listar contas de outro usuario", 403);
        return Result<List<Account>>.Success(await repository.GetAccountsByUserAsync(userId), 200);
    }
}

public class updateAccount(IAccountRepository repository)
{
    public async Task<Result<Account>> update(int id, AccountUpdateDTO dto, int userId)
    {
        var account = await repository.GetAccount(id);
        if (account == null) return Result<Account>.Failure("Conta nao encontrada", 404);
        if (account.userId != userId) return Result<Account>.Failure("Usuario nao pode alterar conta de outro usuario", 403);
        if (string.IsNullOrWhiteSpace(dto.name)) return Result<Account>.Failure("Nome da conta nao pode ser vazio", 400);

        account.name = dto.name.Trim();
        account.type = dto.type;
        await repository.UpdateAccountAsync(account);
        return Result<Account>.Success(account, 200);
    }
}

public class deleteAccount(IAccountRepository repository)
{
    public async Task<Result<Account>> delete(int id, int userId)
    {
        var account = await repository.GetAccount(id);
        if (account == null) return Result<Account>.Failure("Conta nao encontrada", 404);
        if (account.userId != userId) return Result<Account>.Failure("Usuario nao pode excluir conta de outro usuario", 403);
        await repository.DeleteAccountAsync(account);
        return Result<Account>.NoContent();
    }
}
