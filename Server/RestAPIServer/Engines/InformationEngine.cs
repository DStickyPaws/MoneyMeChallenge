using RestAPIServer.Interface;
using RestAPIServer.Models;

namespace RestAPIServer.Engines;

internal class InformationEngine
{
    private IConfiguration configuration { get; set; }

    public InformationEngine(IConfiguration iConfiguration)
    {
        this.configuration = iConfiguration;    
    }

    private Task<string> GetConnectionstring()
    {
        string Result;
        string? ConnectionString;
        
        ConnectionString = configuration.GetConnectionString("");

        Result = ConnectionString ?? string.Empty;

        return Task.FromResult(Result);
    }

    internal Task<bool> SaveInformation()
    {
        return Task.FromResult(true);
        
    }

    internal Task<IInformation> Find()
    {
        IInformation Result;

        Result = new Information("", "", "", "", "", "", "", "");

        return Task.FromResult(Result);
    }
}
