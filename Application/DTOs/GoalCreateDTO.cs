using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public record GoalCreateDTO(
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        string title,

        [Required]
        [Range(0.01, double.MaxValue)]
        decimal totalAmount,

        [Required]
        [Range(0.01, double.MaxValue)]
        decimal currentAmount,
        
        [Required]
        [Range(1, int.MaxValue)]
        int userId,
        
        [Required]
        DateOnly deadline
    );
}