using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Presentation;

public static class CurrentUser
{
    public static int? GetId(ControllerBase controller)
    {
        var value = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(value, out var id) ? id : null;
    }
}
