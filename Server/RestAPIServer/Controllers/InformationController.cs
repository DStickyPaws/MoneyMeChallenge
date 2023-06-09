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
        Result = Ok("https://google.com");
        return Task.FromResult(Result);
    }

    /**
        <Notes>You need to save data atleast once.</Notes>
    */
    private Task<bool> SaveData()
    {
        return Task.FromResult(true);
    }

    /**
        <Notes>
            If its saved you must provide a same redirection URL
        </Notes>
    */
    private Task<bool> IsExisting() 
    {
        return Task.FromResult(true);
    }

    /**
        <Notes>You somehow need to read the data right?</Notes> 
    */
    private Task<IInformation> ReadData()
    {
        IInformation Result;

        Result = new Information("","","","","","","","");
        
        return Task.FromResult(Result);
    }
}