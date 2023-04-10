using Dapper;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data;
using System.Data.SQLite;

namespace RestAPIServer.Engines;

/// <summary>
/// 
/// </summary>
public class BlacklistedDomainEngine
{
    private const string TableName = "BlacklistedDomains";
    private const string Field1 = "Id";
    private const string Field2 = "EmailDomain";

    /// <summary>
    /// 
    /// </summary>
    private IConfiguration Configuration { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    private string ConnectionString { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<IBlacklistedDomain> BlacklistedDomains { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Configuration"></param>
    public BlacklistedDomainEngine(IConfiguration Configuration)
    {
        this.Configuration = Configuration;
        ConnectionString = GetConnectionString().Result;
        BlacklistedDomains = GetAllBlacklistedDomains().Result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Task<string> GetConnectionString()
    {
        string Result;
        string? ConnectionString;

        ConnectionString = Configuration.GetConnectionString("sqlLite");

        Result = ConnectionString ?? string.Empty;

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="blacklistedDomain"></param>
    /// <returns></returns>
    public Task<IBlacklistedDomain> Create(BlacklistedDomain blacklistedDomain) 
    {
        IBlacklistedDomain Result;

        Result = new BlacklistedDomain(blacklistedDomain.EmailDomain, blacklistedDomain.Id);

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<IBlacklistedDomain> Create(string EmailDomain, long? Id = null) 
    {
        IBlacklistedDomain Result;

        Result = new BlacklistedDomain(EmailDomain, Id);

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="blacklistedDomain"></param>
    /// <returns></returns>
    public Task<bool> IsValid(IBlacklistedDomain blacklistedDomain)
    {
        bool InitialResult, SecondaryResult, Result;

        InitialResult = blacklistedDomain.EmailDomain.Contains('.');

        if (InitialResult)
        {
            SecondaryResult = blacklistedDomain.EmailDomain.Trim().Length > 1;
            if (SecondaryResult) Result = true;
            else Result = false;
        }
        else Result = false;

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="blacklistedDomain"></param>
    /// <returns></returns>
    public Task<bool> IsBlacklisted(IBlacklistedDomain blacklistedDomain)
    {
        bool Result, InitialResult;
        IBlacklistedDomain SecondaryResult;

        InitialResult = IsValid(blacklistedDomain).Result;

        if (InitialResult)
        {
            SecondaryResult = Find(blacklistedDomain).Result;
            if (SecondaryResult.Id == null) Result = false;
            else Result = true;
        }
        else Result = true;

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="blacklistedDomain"></param>
    /// <returns></returns>
    public Task<bool> Save(IBlacklistedDomain blacklistedDomain)
    {
        bool Result, InitialResult, IsExisting;
        IBlacklistedDomain SecondaryResult;

        InitialResult = IsValid(blacklistedDomain).Result;
        SecondaryResult = Find(blacklistedDomain).Result;

        IsExisting = SecondaryResult.Id != null;

        if (InitialResult)
        {
            if (!IsExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"INSERT INTO {TableName} (emaildomain) VALUES (@emaildomain)", blacklistedDomain);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="blacklistedDomain"></param>
    /// <returns></returns>
    public Task<IBlacklistedDomain> Find(IBlacklistedDomain blacklistedDomain)
    {
        BlacklistedDomain? InitialResult;
        IBlacklistedDomain Result;            

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<BlacklistedDomain>($"SELECT * FROM { TableName } WHERE emaildomain=@emaildomain", blacklistedDomain).SingleOrDefault();
            }
            catch 
            {
                throw;
            }
        }

        if (InitialResult == null) Result = Create(blacklistedDomain.EmailDomain).Result;
        else Result = InitialResult;

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

        if (InitialResult == null) Result = Create(string.Empty).Result;
        else Result = InitialResult;

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<IBlacklistedDomain>> GetAllBlacklistedDomains()
    {
        IEnumerable<IBlacklistedDomain> Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            Result = dbConnection.Query<BlacklistedDomain>($"SELECT * FROM {TableName}");
        }

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="blacklistedDomain"></param>
    /// <returns></returns>
    public Task<bool> Update(IBlacklistedDomain blacklistedDomain)
    {
        bool Result, InitialResult, IsExisting;
        IBlacklistedDomain SecondaryResult;

        InitialResult = IsValid(blacklistedDomain).Result;        

        if (InitialResult)
        {
            SecondaryResult = Find(blacklistedDomain).Result;
            if (SecondaryResult.Id == null) IsExisting = false;
            else IsExisting = true;

            if (IsExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"UPDATE {TableName} SET emaildomain=@emaildomain WHERE id=@id", blacklistedDomain);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="blacklistedDomain"></param>
    /// <returns></returns>
    public Task<bool> Delete(IBlacklistedDomain blacklistedDomain)
    {
        bool Result, InitialResult, IsExisting;
        IBlacklistedDomain SecondaryResult;
        long IdPlaceHolder;

        InitialResult = IsValid(blacklistedDomain).Result;
        if (InitialResult)
        {
            IdPlaceHolder = blacklistedDomain.Id ?? 0;
            if (IdPlaceHolder != 0)
            {
                SecondaryResult = Find(IdPlaceHolder).Result;

                IsExisting = SecondaryResult.Id != null;

                if (IsExisting)
                {
                    using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                    {
                        try
                        {
                            dbConnection.Query($"DELETE FROM {TableName} WHERE emaildomain=@emaildomain", blacklistedDomain);
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
