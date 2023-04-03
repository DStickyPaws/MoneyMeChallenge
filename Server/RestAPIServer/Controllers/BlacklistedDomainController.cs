
using Microsoft.AspNetCore.Mvc;
using RestAPIServer.Engines;
using RestAPIServer.Interface;

namespace RestAPIServer.Controllers;

[ApiController]
[Route("[controller]")]    
public class BlacklistedDomainController : ControllerBase
{
    private IConfiguration Configuration { get; set; }
    private BlacklistedDomainEngine Engine { get; set; }
    
    public BlacklistedDomainController(IConfiguration Configurration)
    {
        this.Configuration = Configurration;
        Engine = new BlacklistedDomainEngine(Configuration);
    }

    [HttpGet]
    public Task<IQueryable<IBlacklistedDomain>> GetBlacklistedDomains()
    {
        List<IBlacklistedDomain> Result;

        Result = new List<IBlacklistedDomain>();

        return Task.FromResult((IQueryable<IBlacklistedDomain>)Result);
    }
}
