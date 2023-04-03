
using Microsoft.AspNetCore.Mvc;
using RestAPIServer.Engines;
using RestAPIServer.Interface;

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
}
