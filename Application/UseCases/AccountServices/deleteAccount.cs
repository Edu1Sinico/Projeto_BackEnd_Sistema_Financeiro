using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

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