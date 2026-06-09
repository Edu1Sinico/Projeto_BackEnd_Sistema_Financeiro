using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

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