using Domain.Models;

namespace Application.DTOs;

public record CategoryListDTO(List<Category> categories,int page, int quantity);