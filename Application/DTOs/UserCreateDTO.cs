using System.ComponentModel.DataAnnotations;


namespace Application.DTOs
{
    public record UserCreateDTO(
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        string name,

        [Required]
        [EmailAddress]
        string email,
        
        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        string password
    );
}