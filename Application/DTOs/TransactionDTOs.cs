using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs;

public record TransactionCreateDTO([Required] string description, [Range(0.01, double.MaxValue)] decimal amount, [Required] TransactionType type, [Range(1, int.MaxValue)] int categoryId, [Range(1, int.MaxValue)] int accountId);
public record TransactionUpdateDTO([Required] string description, [Range(0.01, double.MaxValue)] decimal amount, [Required] TransactionType type, [Range(1, int.MaxValue)] int categoryId, [Range(1, int.MaxValue)] int accountId);
