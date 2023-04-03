using Microsoft.AspNetCore.Mvc;
using RestAPIServer.Engines;
using RestAPIServer.Interface;

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
}
