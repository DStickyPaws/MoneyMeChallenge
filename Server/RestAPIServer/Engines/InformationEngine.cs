using RestAPIServer.Interface;
using RestAPIServer.Models;

namespace RestAPIServer.Engines;

internal class InformationEngine
{
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
