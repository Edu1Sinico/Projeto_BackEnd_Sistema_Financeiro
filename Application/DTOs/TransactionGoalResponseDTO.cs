namespace Application.DTOs
{
    public record TransactionGoalResponseDTO(int transactionId, int goalId, decimal contributedAmount);
}