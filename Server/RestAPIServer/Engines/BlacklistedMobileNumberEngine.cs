using Dapper;
using System.Data.SQLite;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data;

namespace RestAPIServer.Engines;

internal class BlacklistedMobileNumberEngine
{
    private IConfiguration configuration { get; set; }
    private string ConnectionString { get; set; }
    public IEnumerable<IBlacklistMobilenumber> BlacklistedMobilenumbers { get; private set; }


    public BlacklistedMobileNumberEngine(IConfiguration configuration)
    {
        this.configuration = configuration;
        ConnectionString = GetConnectionString().Result;

        BlacklistedMobilenumbers = GetAllBlackListedMobileNumber().Result;
    }

    private Task<string> GetConnectionString()
    {
        string Result;
        string? ConnectionString;

        ConnectionString = configuration.GetConnectionString("sqlLite");

        Result = ConnectionString ?? string.Empty;

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

    public Task<IEnumerable<IBlacklistMobilenumber>> GetAllBlackListedMobileNumber()
    {
        IEnumerable<IBlacklistMobilenumber> Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                Result = (IEnumerable<BlacklistedMobileNumber>)dbConnection.Query<BlacklistedMobileNumber>("select * from BlacklistedMobileNumbers");
            }
            catch
            {
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    public Task<bool> IsBlacklisted(IBlacklistMobilenumber Mobilenumber)
    {
        bool Result;

        Result = true;

        return Task.FromResult(Result);
    }
}
