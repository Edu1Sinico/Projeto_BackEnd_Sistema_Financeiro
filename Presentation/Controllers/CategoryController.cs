using Application.DTOs;
using Application.UseCases.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("category")]
public class CategoryController(createCategory createCategory, getCategory getCategory, getCategories getCategories, updateCategory updateCategory, deleteCategory deleteCategory) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CategoryCreateDTO dto)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await createCategory.create(dto, userId.Value), this);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await getCategory.getOne(id, userId.Value), this);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> List([FromQuery] int userId)
    {
        var authenticatedUserId = CurrentUser.GetId(this);
        if (authenticatedUserId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await getCategories.getMany(userId, authenticatedUserId.Value), this);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryUpdateDTO dto)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await updateCategory.update(id, dto, userId.Value), this);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var userId = CurrentUser.GetId(this);
        if (userId == null) return Unauthorized();
        return HttpResponseMapper.createResponse(await deleteCategory.delete(id, userId.Value), this);
    }
}
