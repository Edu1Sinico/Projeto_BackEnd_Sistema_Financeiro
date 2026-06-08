using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation;

public static class HttpResponseMapper
{
    public static IActionResult createResponse<T>(Result<T> result, ControllerBase controller)
    {
        return result.code switch
        {
            200 => controller.Ok(result.value),
            201 => controller.StatusCode(StatusCodes.Status201Created, result.value),
            204 => controller.NoContent(),
            400 => controller.BadRequest(result.error),
            401 => controller.Unauthorized(),
            403 => controller.StatusCode(StatusCodes.Status403Forbidden, result.error),
            404 => controller.NotFound(result.error),
            409 => controller.Conflict(result.error),
            _ => controller.StatusCode(500)
        };
    }
}
