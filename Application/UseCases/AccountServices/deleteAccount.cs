using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

public class deleteAccount(IAccountRepository repository)
{
    public async Task<Result<Account>> delete(int id)
    {
        await repository.DeleteAccountAsync(id);
        return Result<Account>.NoContent();
    }
}