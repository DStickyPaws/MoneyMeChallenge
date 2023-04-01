using Microsoft.AspNetCore.Mvc;
using RestAPIServer.Interface;
using RestAPIServer.Models;

namespace RestAPIServer.Controllers;

[ApiController]
[Route("[controller]")]
public class InformationController : ControllerBase 
{
    [HttpPost]
    [Route("AskForQuotation")]
    public Task<IActionResult> AskForQuotation([FromBody] Information Info)
    {
        IActionResult Result;
        Result = Ok();
        return Task.FromResult(Result);
    }
}