using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

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