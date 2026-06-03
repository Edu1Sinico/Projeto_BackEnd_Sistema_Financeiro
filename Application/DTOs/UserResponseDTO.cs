using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record UserResponseDTO(int Id, string name, string email, DateOnly creationDate);
}