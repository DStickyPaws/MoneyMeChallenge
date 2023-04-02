using Microsoft.AspNetCore.Mvc;
using RestAPIServer.Engines;
using RestAPIServer.Interface;

namespace RestAPIServer.Controllers;

[ApiController]
[Route("[controller]")]
public class BlacklistedMobilenumberController : Controller
{
    private IConfiguration Configuration { get; set; }
    private BlacklistedMobileNumberEngine Engine { get; set; }

    public BlacklistedMobilenumberController(IConfiguration Configuration)
    {
        this.Configuration = Configuration;
        Engine = new BlacklistedMobileNumberEngine(this.Configuration);
    }

    [HttpGet]
    [Route("BlacklistedMobilenumbers")]
    public Task<IQueryable<IBlacklistMobilenumber>> GetAllBlacklistedMobilenumbers()
    {
        IQueryable<IBlacklistMobilenumber> Result;

        bool TaskResponse;

        TaskResponse = Engine.GetAllBlackListedMobileNumber().Result;

        if (TaskResponse) Result = Engine.BlacklistedMobilenumbers;
        else Result = (IQueryable<IBlacklistMobilenumber>)new List<IBlacklistMobilenumber>();

        return Task.FromResult(Result);
    }
}
