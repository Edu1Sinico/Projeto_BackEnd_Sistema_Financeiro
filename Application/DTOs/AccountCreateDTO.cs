using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs
{
    public record AccountCreateDTO(
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        string name,
        [Required]
        [Range(0,double.MaxValue)]
        decimal balance,
        [Required]
        AccountType type,
        [Required]
        [Range(1,int.MaxValue)]
        int userId
    );
}