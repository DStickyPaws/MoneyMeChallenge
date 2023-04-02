using RestAPIServer.Interface;
using RestAPIServer.Models;

namespace RestAPIServer.Engines;

internal class BlacklistedMobileNumberEngine
{
    private IConfiguration configuration;
    private string ConnectionString;
    public IQueryable<IBlacklistMobilenumber> BlacklistedMobilenumbers { get; private set; }


    public BlacklistedMobileNumberEngine(IConfiguration configuration)
    {
        this.configuration = configuration;
        ConnectionString = GetConnectionString().Result;
        this.BlacklistedMobilenumbers = (IQueryable<IBlacklistMobilenumber>)new List<IBlacklistMobilenumber>();
    }

    private Task<string> GetConnectionString()
    {
        string Result;

        Result = string.Empty;

        return Task.FromResult(Result);
    }

    public Task<bool> Save(IBlacklistMobilenumber Mobilenumber)
    {
        bool Result;

        Result = true;

        return Task.FromResult(Result);
    }

    public Task<IBlacklistMobilenumber> Find(IBlacklistMobilenumber Mobilenumber)
    {
        IBlacklistMobilenumber Result;

        Result = new BlacklistedMobileNumber("");

        return Task.FromResult(Result);
    }

    public Task<bool> GetAllBlackListedMobileNumber()
    {
        bool Result;

        Result = true;

        return Task.FromResult(Result);
    }

    public Task<bool> IsBlacklisted(IBlacklistMobilenumber Mobilenumber)
    {
        bool Result;

        Result = true;

        return Task.FromResult(Result);
    }
}
