using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record UserCreateDTO([Required] string name, [Required, EmailAddress] string email, [Required, MinLength(8)] string password);
public record UserUpdateDTO([Required] string name, [Required, EmailAddress] string email);
public record UserResponseDTO(int id, string name, string email, DateOnly creationDate);
