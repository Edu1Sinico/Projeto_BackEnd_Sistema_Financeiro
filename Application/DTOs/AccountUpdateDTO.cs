using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs
{
    public record AccountUpdateDTO(
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        string name,
        
        [Required]
        AccountType type
    );

    // O valor do saldo final será calculado no service
}
