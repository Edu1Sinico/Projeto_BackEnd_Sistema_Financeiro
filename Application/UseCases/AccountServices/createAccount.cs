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