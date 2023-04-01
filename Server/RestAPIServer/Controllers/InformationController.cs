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
    public Task<IInformation> AskForQuotation([FromBody] Information Info)
    {
        return Task.FromResult((IInformation)Info);
        // IActionResult Result;
        // Result = Ok("Hello!");
        // return Task.FromResult(Result);
    }
}