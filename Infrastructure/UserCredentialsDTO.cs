using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record UserCredentialsDTO([Required(AllowEmptyStrings = false)]string email, [Required(AllowEmptyStrings = false)]string password);