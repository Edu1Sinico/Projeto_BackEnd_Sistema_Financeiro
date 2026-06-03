using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

public class createAccount(IAccountRepository repository)
{
    public async Task<Result<Account>> create(AccountCreateDTO dto)
    {
        var account = new Account(dto.name, dto.balance, dto.type, dto.userId);
        await repository.CreateAccountAsync(account);
        
        return Result<Account>.Success(account, 201);
    }
}