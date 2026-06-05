using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public record TransactionGoalCreateDTO(
        [Required]
        [Range(1, int.MaxValue)]
        int transactionId,

        [Required]
        [Range(1, int.MaxValue)]
        int goalId,

        [Required]
        [Range(0.01, double.MaxValue)]
        decimal contributedAmount
    );
}