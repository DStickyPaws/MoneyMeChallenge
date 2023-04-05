using Microsoft.AspNetCore.Mvc;
using RestAPIServer.Engines;
using RestAPIServer.Interface;
using RestAPIServer.Models;

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
    public Task<IEnumerable<IBlacklistMobilenumber>> GetAllBlacklistedMobilenumbers()
    {
        IEnumerable<IBlacklistMobilenumber> Result;

        Result = Engine.BlacklistedMobilenumbers;

        return Task.FromResult(Result);
    }

    [HttpPost]
    [Route("IsValid")]
    public Task<bool> IsValid([FromBody] BlacklistedMobileNumber mobileNumber)
    {
        bool Result;

        Result = Engine.ValidateMobileNumber((IBlacklistMobilenumber)mobileNumber).Result;

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
    public Task<IActionResult> UpdateMobileNumber(long id, [FromBody]  BlacklistedMobileNumber newMobileNumber)
    {
        bool SecondaryResult, TertiaryResult;
        IActionResult Result;
        IBlacklistMobilenumber InitialResult;
        long IdPlaceHolder;

        InitialResult = Engine.Find(id).Result;
        SecondaryResult = InitialResult.id != null ? true : false;
        if (SecondaryResult)
        {
            IdPlaceHolder = InitialResult.id ?? 0;
            TertiaryResult = Engine.Update(BlacklistedMobileNumber.Create(IdPlaceHolder, newMobileNumber.mobilenumber).Result).Result;
            if (TertiaryResult) Result = Ok("Successful Update.");
            else Result = StatusCode(500, "Something went wrong during update");
        }
        else Result = StatusCode(409, "The number you have submitted to update does not exist");
        

        return Task.FromResult(Result);
    }

    [HttpDelete]
    [Route("RemoveFromBlacklist")]
    public Task<IActionResult> RemoveMobileNumberFromBlacklist([FromBody] BlacklistedMobileNumber mobileNumber)
    {
        bool InitialResult, SecondaryResult;
        IActionResult Result;

        InitialResult = Engine.IsBlacklisted(mobileNumber).Result;

        if (InitialResult)
        {
            SecondaryResult = Engine.Delete(mobileNumber).Result;
            if (SecondaryResult) Result = Ok("Sucessful Deletion.");
            else Result = StatusCode(500, "Something went wrong during deletion.");
        }
        else Result = StatusCode(409, "The number you have submitted to blacklist does not exist, and conflicts with the current serve state.");

        return Task.FromResult(Result);
    }

    [HttpDelete]
    [Route("RemoveFromBlacklistById")]
    public Task<IActionResult> RemoveMobileNumberFromBlacklist(long id)
    {
        bool SecondaryResult;
        IBlacklistMobilenumber InitialResult;
        IActionResult Result;

        InitialResult = Engine.Find(id).Result;
        SecondaryResult = Engine.IsBlacklisted(InitialResult).Result;

        if (SecondaryResult)
        {
            SecondaryResult = Engine.Delete(InitialResult).Result;
            if (SecondaryResult) Result = Ok("Sucessful Deletion.");
            else Result = StatusCode(500, "Something went wrong during deletion.");
        }
        else Result = StatusCode(409, "The number you have submitted to blacklist does not exist, and conflicts with the current serve state.");

        return Task.FromResult(Result);
    }
}
