using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("goal")]
public class GoalController : ControllerBase
{
    [HttpPost("")]
    public IActionResult post()
    {
        return HttpResponseMapper(,this); 
    }
    
    [HttpGet("{id:int}")]
    public IActionResult get()
    {
        return HttpResponseMapper(,this);
    }
    
    [HttpGet]
    public IActionResult list()
    {
        return HttpResponseMapper(,this);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult update()
    {
        return;
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult delete()
    {
        return HttpResponseMapper(,this);
    }

}