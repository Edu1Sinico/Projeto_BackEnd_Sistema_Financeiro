namespace Application.DTOs
{
    public record UserResponseDTO(int id, string name, string email, DateOnly creationDate);
}