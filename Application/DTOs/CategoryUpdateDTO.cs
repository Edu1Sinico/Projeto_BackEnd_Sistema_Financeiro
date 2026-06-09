using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs;

public record CategoryUpdateDTO([Required] string name, [Required] TransactionType type);