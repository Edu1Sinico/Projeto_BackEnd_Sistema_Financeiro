using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation;

public static class HttpResponseMapper
{
    public static IActionResult createResponse<T>(Result<T> result, Controller controller)
    {
        return result.code switch
        {
            200 => controller.Ok(),
            201 => controller.Created(),
            404 => controller.NotFound(),
            400 => controller.BadRequest(),
            401 => controller.Unauthorized(),
            403 => controller.Forbid(),
            409 => controller.Conflict(),
            _ => controller.StatusCode(500)

        };

    }
}