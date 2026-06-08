using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record GoalCreateDTO([Required] string title, [Range(0.01, double.MaxValue)] decimal totalAmount, [Range(0, double.MaxValue)] decimal currentAmount, DateOnly? deadline, [Range(1, int.MaxValue)] int userId);
public record GoalUpdateDTO([Required] string title, [Range(0.01, double.MaxValue)] decimal totalAmount, [Range(0, double.MaxValue)] decimal currentAmount, DateOnly? deadline);
