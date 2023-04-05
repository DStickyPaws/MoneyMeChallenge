
using Microsoft.AspNetCore.Mvc;
using RestAPIServer.Engines;
using RestAPIServer.Interface;
using RestAPIServer.Models;

namespace RestAPIServer.Controllers;

[ApiController]
[Route("[controller]")]
public class BlacklistedDomainController : ControllerBase
{
    private IConfiguration Configuration { get; set; }
    private BlacklistedDomainEngine Engines { get; set; }

    public BlacklistedDomainController(IConfiguration Configurration)
    {
        this.Configuration = Configurration;
        Engines = new BlacklistedDomainEngine(Configuration);
    }

    [HttpGet]
    [Route("GetBlacklistedDomains")]
    public Task<IEnumerable<IBlacklistedDomain>> GetBlacklistedDomains()
    {
        IEnumerable<IBlacklistedDomain> Result;

        Result = Engines.BlacklistedDomains;

        return Task.FromResult(Result);
    }

    [HttpPost]
    [Route("IsValid")]
    public Task<bool> IsValid([FromBody] BlacklistedDomain blacklistedDomain)
    {
        bool Result;

        Result = Engines.IsValid(blacklistedDomain).Result;

        return Task.FromResult(Result);
    }

    [HttpPost]
    [Route("BlacklistDomain")]
    public Task<IActionResult> BlaclistDomain([FromBody] BlacklistedDomain blacklistedDomain)
    {
        IActionResult Result;
        bool InitialResult;

        InitialResult = Engines.Save(blacklistedDomain).Result;

        if (InitialResult) Result = Ok("Successful blacklisting of domain");
        else Result = StatusCode(500, "Something went wrong in blacklisting the domain.");

        return Task.FromResult(Result);
    }

    [HttpPost]
    [Route("IsBlacklisted")]
    public Task<bool> IsBlacklisted([FromBody] BlacklistedDomain blacklistedDomain)
    {
        bool Result;

        Result = Engines.IsBlacklisted(blacklistedDomain).Result;

        return Task.FromResult(Result);
    }

    [HttpDelete]
    [Route("RemoveFromBlacklist")]
    public Task<IActionResult> RemoveFromBlacklist([FromBody] BlacklistedDomain blacklistedDomain)
    {
        IActionResult Result;
        bool InitialResult;

        InitialResult = Engines.Delete(blacklistedDomain).Result;

        if (InitialResult) Result = Ok("Sucessful in removing the blacklist");
        else Result = StatusCode(500, "Something went wrong when removing the blacklist");

        return Task.FromResult(Result);
    }

    [HttpDelete]
    [Route("RemoveFromBlacklistById")]
    public Task<IActionResult> RemoveFromBlacklist(long Id)
    {        
        bool SecondaryResult, IsExisting;
        IActionResult Result;
        IBlacklistedDomain InitialResult;

        InitialResult = Engines.Find(Id).Result;
        IsExisting = InitialResult.id != null;
        if (IsExisting) SecondaryResult = Engines.Delete(InitialResult).Result;
        else SecondaryResult = false;

        if (SecondaryResult) Result = Ok("Sucessful in removing the blacklist");
        else Result = StatusCode(500, "Something went wrong when removing the blacklist");

        return Task.FromResult(Result);
    }
}
