using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs;

public record CategoryCreateDTO([Required] string name, [Required] TransactionType type, [Range(1, int.MaxValue)] int userId);
