namespace Application.DTOs
{
    public record GoalResponseDTO(int id, string title, decimal totalAmount, decimal currentAmount, int userId);
}