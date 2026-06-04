using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

public class deleteAccount(IAccountRepository repository)
{
    public async Task<Result<Account>> delete(int id)
    {
        var account = await repository.GetAccount(id);
        if (account == null) { return Result<Account>.Failure("Conta não encontrada", 404); }

        await repository.DeleteAccountAsync(account);
        return Result<Account>.NoContent();
    }
}