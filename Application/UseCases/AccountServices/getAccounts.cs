using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.AccountServices;

public class getAccounts(IAccountRepository repository)
{
    public async Task<Result<AccountListDTO>> getMany(int userId, int authenticatedUserId,int page, int quantity)
    {
        if (userId != authenticatedUserId) return Result<AccountListDTO>.Failure("Usuario nao pode listar contas de outro usuario", 403);
        return Result<AccountListDTO>.Success(new AccountListDTO(
            await repository.GetAccountsByUserAsync(userId,page,quantity),page, quantity),
            200);
    }
}