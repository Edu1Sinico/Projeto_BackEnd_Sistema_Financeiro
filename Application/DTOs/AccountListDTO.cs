using Domain.Models;

namespace Application.DTOs;

public record AccountListDTO(List<Account> accounts,int page, int quantity);