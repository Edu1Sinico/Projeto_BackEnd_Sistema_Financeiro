using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

public class updateAccount(IAccountRepository repository)
{
    public async Task<Result<Account>> update(int id, AccountUpdateDTO dto)
    {
        var account = repository.GetAccount(id);
        
    }
}