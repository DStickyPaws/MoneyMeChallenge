using Microsoft.AspNetCore.Mvc;
using RestAPIServer.Engines;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data.SQLite;
using System.Data;

namespace RestAPIServer.Controllers;

[ApiController]
[Route("[controller]")]
public class BlacklistedMobilenumberController : ControllerBase
{
    private IConfiguration Configuration { get; set; }
    private BlacklistedMobileNumberEngine Engine { get; set; }


    public BlacklistedMobilenumberController(IConfiguration Configuration)
    {
        this.Configuration = Configuration;
        Engine = new BlacklistedMobileNumberEngine(this.Configuration);
    }

    [HttpGet]
    [Route("GetAllBlacklistedMobilenumbers")]
    public Task<IEnumerable<IBlacklistMobileNumber>> GetAllBlacklistedMobilenumbers()
    {
        IEnumerable<IBlacklistMobileNumber> Result;

        Result = Engine.GetAllBlackListedMobileNumber().Result;

        return Task.FromResult(Result);
    }

    [HttpPost]
    [Route("IsValid")]
    public Task<bool> IsValid([FromBody] BlacklistedMobileNumber mobileNumber)
    {
        bool Result;
        
        Result = Engine.IsValid(mobileNumber).Result;

        return Task.FromResult(Result);
    }

    [HttpPost]
    [Route("IsBlacklisted")]
    public Task<bool> IsBlacklisted([FromBody] BlacklistedMobileNumber mobileNumber)
    {
        bool Result;

        Result = Engine.IsBlacklisted(mobileNumber).Result;

        return Task.FromResult(Result);
    }

    [HttpPost]
    [Route("AddToMobilenumberBlacklist")]
    public Task<IActionResult> AddToMobileNumberBlacklist([FromBody] BlacklistedMobileNumber mobileNumber)
    {
        bool InitialResult;

        IActionResult Result;

        InitialResult = Engine.Save(mobileNumber).Result;

        if (InitialResult) Result = Created("AddToMobilenumberBlacklist", mobileNumber);
        else Result = StatusCode(500, "Cannot be saved");
        return Task.FromResult(Result);
    }

    [HttpPatch]
    [Route("UpdateMobileNumber")]
    public Task<IActionResult> UpdateMobileNumber([FromQuery] long id, [FromBody]  BlacklistedMobileNumber newMobileNumber)
    {
        bool isSuccessful;
        IActionResult Result;        

        isSuccessful = Engine.Update(id, newMobileNumber.MobileNumber).Result;

        if (isSuccessful) Result = Ok("Successful Update");
        else Result = StatusCode(500, "Something went wrong during update");

        return Task.FromResult(Result);
    }

    [HttpDelete]
    [Route("RemoveFromBlacklist")]
    public Task<IActionResult> RemoveMobileNumberFromBlacklist([FromBody] BlacklistedMobileNumber mobileNumber)
    {
        bool isSuccessful;
        IActionResult Result;

        isSuccessful = Engine.Delete(mobileNumber).Result;

        if (isSuccessful) Result = Ok("Successful Removal from Blacklist");
        else Result = StatusCode(500, "Something went wrong during removal from blacklist");

        return Task.FromResult(Result);
    }

    [HttpDelete]
    [Route("RemoveFromBlacklistById")]
    public Task<IActionResult> RemoveMobileNumberFromBlacklist(long id)
    {
        bool isSuccessful;
        IActionResult Result;

        isSuccessful = Engine.Delete(id).Result;
        if (isSuccessful) Result = Ok("Successful Removal from Blacklist");
        else Result = StatusCode(500, "Something went wrong during removal from blacklist");

        return Task.FromResult(Result);
    }
}
