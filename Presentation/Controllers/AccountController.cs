using Application.UseCases.Account;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    [HttpPost("")]
    public IActionResult post()
    {
        return HttpResponseMapper(createAccount,this);
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
        return HttpResponseMapper(,this);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult delete()
    {
        return HttpResponseMapper(,this);
    }
    
    
    
}