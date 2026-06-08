using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs;

public record AccountCreateDTO([Required] string name, [Range(0, double.MaxValue)] decimal balance, [Required] AccountType type, [Range(1, int.MaxValue)] int userId);
public record AccountUpdateDTO([Required] string name, [Required] AccountType type);
