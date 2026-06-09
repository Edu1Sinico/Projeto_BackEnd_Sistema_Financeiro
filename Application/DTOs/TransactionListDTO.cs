using Domain.Models;

namespace Application.DTOs;

public record TransactionListDTO(List<Transaction> transactions,int page, int quantity);