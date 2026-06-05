using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public record GoalUpdateDTO(
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    string title,
    
    [Required]
    [Range(0.01, double.MaxValue)]
    decimal totalAmount,

    [Required]
    [Range(0, double.MaxValue)]
    decimal currentAmount
);
}