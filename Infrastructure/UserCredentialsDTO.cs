using System.ComponentModel.DataAnnotations;

namespace Infrastructure;

public record UserCredentialsDTO([Required, EmailAddress] string email, [Required] string password);
