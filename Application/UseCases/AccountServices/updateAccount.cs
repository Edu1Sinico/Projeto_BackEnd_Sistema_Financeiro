using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

public class updateAccount(IAccountRepository repository)
{
    public async Task<Result<Account>> update(int id, AccountUpdateDTO dto)
    {
        var account = await repository.GetAccount(id);
        
        if (account == null) { return Result<Account>.Failure("Conta não encontrada", 404); }
                
        account.name = dto.name;
        account.balance = dto.balance;
        account.type = dto.type;

        await repository.UpdateAccountAsync(account);

        return Result<Account>.Success(account, 200);
    }
}