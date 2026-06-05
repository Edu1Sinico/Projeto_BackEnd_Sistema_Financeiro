using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public record UserUpdateDTO(
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        string name,
        
        [Required]
        [EmailAddress]
        string email
    );
}