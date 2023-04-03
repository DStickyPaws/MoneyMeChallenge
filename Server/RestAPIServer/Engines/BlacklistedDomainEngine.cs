using Dapper;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data;
using System.Data.SQLite;

namespace RestAPIServer.Engines;

public class BlacklistedDomainEngine
{
    private const string TableName = "BlacklistedDomains";
    private IConfiguration Configuration { get; set; }
    private string ConnectionString { get; set; }

    public IEnumerable<IBlacklistedDomain> BlacklistedDomains { get; set; }

    public BlacklistedDomainEngine(IConfiguration Configuration)
    {
        this.Configuration = Configuration;
        ConnectionString = GetConnectionString().Result;
        BlacklistedDomains = GetAllBlacklistedDomains().Result;
    }

    private Task<string> GetConnectionString()
    {
        string Result;
        string? ConnectionString;

        ConnectionString = Configuration.GetConnectionString("sqlLite");

        Result = ConnectionString ?? string.Empty;

        return Task.FromResult(Result);
    }

    public Task<bool> IsValid(IBlacklistedDomain blacklistedDomain)
    {
        bool InitialResult, SecondaryResult, Result;

        InitialResult = blacklistedDomain.emaildomains.Contains('.');

        if (InitialResult)
        {
            SecondaryResult = blacklistedDomain.emaildomains.Trim().Length > 1;
            if (SecondaryResult) Result = true;
            else Result = false;
        }
        else Result = false;

        return Task.FromResult(Result);
    }

    public Task<bool> IsBlacklisted(IBlacklistedDomain blacklistedDomain)
    {
        bool Result, InitialResult;
        IBlacklistedDomain SecondaryResult;

        InitialResult = IsValid(blacklistedDomain).Result;

        if (InitialResult)
        {
            SecondaryResult = Find(blacklistedDomain).Result;
            if (SecondaryResult.id == null) Result = false;
            else Result = true;
        }
        else Result = true;

        return Task.FromResult(Result);
    }

    public Task<bool> Save(IBlacklistedDomain blacklistedDomain)
    {
        bool Result, InitialResult, IsExisting;
        IBlacklistedDomain SecondaryResult;

        InitialResult = IsValid(blacklistedDomain).Result;
        SecondaryResult = Find(blacklistedDomain).Result;

        IsExisting = SecondaryResult != null;

        if (InitialResult)
        {
            if (!IsExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"INSERT INTO {TableName} (emaildomains) VALUES (@emailddomains)", blacklistedDomain);
                        Result = true;
                    }
                    catch
                    {
                        Result = false;
                        throw;
                    }
                }
            }
            else Result = false;
        }
        else Result = false;

        return Task.FromResult(Result);
    }

    public Task<IBlacklistedDomain> Find(IBlacklistedDomain blacklistedDomain)
    {
        BlacklistedDomain? InitialResult;
        IBlacklistedDomain Result;            

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<BlacklistedDomain>($"SELECT * FROM { TableName } WHERE emaildomains=@emaildomains", blacklistedDomain).SingleOrDefault();
            }
            catch 
            {
                throw;
            }
        }

        if (InitialResult == null) Result = BlacklistedDomain.Create(blacklistedDomain.emaildomains).Result;
        else Result = InitialResult;

        return Task.FromResult(Result);
    }

    public Task<IBlacklistedDomain> Find(long id)
    {
        BlacklistedDomain? InitialResult;
        IBlacklistedDomain Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<BlacklistedDomain>($"SELECT * FROM {TableName} WHERE id=@id", id).SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        if (InitialResult == null) Result = BlacklistedDomain.Create(string.Empty).Result;
        else Result = InitialResult;

        return Task.FromResult(Result);
    }

    public Task<IEnumerable<IBlacklistedDomain>> GetAllBlacklistedDomains()
    {
        IEnumerable<IBlacklistedDomain> Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            Result = dbConnection.Query<BlacklistedDomain>($"SELECT * FROM {TableName}");
        }

        return Task.FromResult(Result);
    }

    public Task<bool> Update(IBlacklistedDomain blacklistedDomain)
    {
        bool Result, InitialResult, IsExisting;
        IBlacklistedDomain SecondaryResult;

        InitialResult = IsValid(blacklistedDomain).Result;        

        if (InitialResult)
        {
            SecondaryResult = Find(blacklistedDomain).Result;
            if (SecondaryResult.id == null) IsExisting = false;
            else IsExisting = true;

            if (IsExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"UPDATE {TableName} SET emaildomains=@emaildomains WHERE id=@id", blacklistedDomain);
                        Result = true;
                    }
                    catch
                    {
                        Result = false;
                        throw;
                    }
                }
            }
            else Result = false;
        }
        else Result = false;

        return Task.FromResult(Result);
    }

    public Task<bool> Delete(IBlacklistedDomain blacklistedDomain)
    {
        bool Result, InitialResult, IsExisting;
        IBlacklistedDomain SecondaryResult;
        long IdPlaceHolder;

        InitialResult = IsValid(blacklistedDomain).Result;
        if (InitialResult)
        {
            IdPlaceHolder = blacklistedDomain.id ?? 0;
            if (IdPlaceHolder != 0)
            {
                SecondaryResult = Find(IdPlaceHolder).Result;

                IsExisting = SecondaryResult.id != null;

                if (IsExisting)
                {
                    using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                    {
                        try
                        {
                            dbConnection.Query($"DELETE FROM {TableName} WHERE emaildomains=@emaildomains", blacklistedDomain);
                            Result = true;
                        }
                        catch
                        {
                            Result = false;
                            throw;
                        }
                    }
                }
                else Result = false;
            }
            else Result = false;
        }
        else Result = false;

        return Task.FromResult(Result);
    }
}
