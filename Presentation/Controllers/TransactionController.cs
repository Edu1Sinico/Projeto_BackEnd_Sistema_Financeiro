using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("transaction")]
public class TransactionController : ControllerBase
{
    [HttpPost("")]
    public IActionResult post()
    {
        return;
    }
    
    [HttpGet("{id:int}")]
    public IActionResult get()
    {
        return;
    }
    
    [HttpGet]
    public IActionResult list()
    {
        return;
    }
    
    [HttpPut("{id:int}")]
    public IActionResult update()
    {
        return;
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult delete()
    {
        return;
    }

}