using Domain.Models;

namespace Application.DTOs;

public record GoalListDTO(List<Goal> goals,int page, int quantity);