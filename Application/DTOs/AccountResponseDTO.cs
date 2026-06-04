using Domain.Models;

namespace Application.DTOs
{
    public record AccountResponseDTO(int id, string name, decimal balance, AccountType type, int userId, string userName);
}