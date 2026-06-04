using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs
{
    public record TransactionCreateDTO(
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        string description,
        [Required]
        [Range(0,double.MaxValue)]
        decimal amount,
        [Required]
        TransactionType type,
        [Required]
        TransactionType category,
        [Required]
        [Range(1,int.MaxValue)]
        int accountId
    );
}