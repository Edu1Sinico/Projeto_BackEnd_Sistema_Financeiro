using Domain.Models;

namespace Application.DTOs
{
    public record TransactionResponseDTO(int id, string description, decimal amount, TransactionType type, DateOnly transactionDate, Category category, int accountId, string accountName);
}