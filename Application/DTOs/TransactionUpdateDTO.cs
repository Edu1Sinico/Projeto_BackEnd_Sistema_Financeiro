using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs
{
    public record TransactionUpdateDTO(
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        string description,

        [Required]
        [Range(0.01, double.MaxValue)]
        decimal amount,

        [Required]
        TransactionType type,

        [Required]
        TransactionType category,
        
        [Required]
        [Range(1,int.MaxValue)]
        int accountId,
        
        [Required]
        [Range(1,int.MaxValue)]
        int categoryId
    );
}