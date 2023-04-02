using RestAPIServer.Interface;

namespace RestAPIServer.Engines;

internal class InformationEngine
{
    internal Task<bool> SaveInformation()
    {
        return Task.FromResult(true);
        
    }

    internal Task<IInformation> Find()
    {
        
    }
}
