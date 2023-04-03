using Dapper;
using System.Data.SQLite;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data;

namespace RestAPIServer.Engines;

internal class BlacklistedMobileNumberEngine
{
    private const string TableName = "BlacklistedMobileNumbers";

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

    public Task<bool> ValidateMobileNumber(IBlacklistMobilenumber MobileNumber)
    {
        bool Result;

        Result = true;

        foreach (char c in MobileNumber.mobilenumber) 
        {
            if (!char.IsNumber(c))
            {
                if (c != '+') Result = false; break;
            }
        }

        return Task.FromResult(Result);
    }

    public Task<bool> Save(IBlacklistMobilenumber Mobilenumber)
    {
        bool Result, InitialResult;

        InitialResult = ValidateMobileNumber(Mobilenumber).Result;

        if (InitialResult)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
            {
                try
                {
                    dbConnection.Query<BlacklistedMobileNumber>($"INSERT INTO {TableName} (mobilenumber) VALUES (@mobilenumber)", Mobilenumber);
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

        return Task.FromResult(Result);
    }    

    public Task<IBlacklistMobilenumber> Find(IBlacklistMobilenumber Mobilenumber)
    {
        IBlacklistMobilenumber Result;
        IBlacklistMobilenumber? InitialResult;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<BlacklistedMobileNumber>($"SELECT * FROM {TableName} WHERE mobilenumber=@mobilenumber", Mobilenumber).SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        if (InitialResult != null) Result = InitialResult;
        else Result = Mobilenumber;

        return Task.FromResult(Result);
    }

    public Task<IBlacklistMobilenumber> Find(long id)
    {
        IBlacklistMobilenumber Result;
        IBlacklistMobilenumber? InitialResult;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<BlacklistedMobileNumber>($"SELECT * FROM {TableName} WHERE id={id}").SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        if (InitialResult != null) Result = InitialResult;
        else Result = BlacklistedMobileNumber.Create(id, string.Empty).Result;

        return Task.FromResult(Result);
    }

    public Task<IEnumerable<IBlacklistMobilenumber>> GetAllBlackListedMobileNumber()
    {
        IEnumerable<IBlacklistMobilenumber> Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                Result = (IEnumerable<BlacklistedMobileNumber>)dbConnection.Query<BlacklistedMobileNumber>($"SELECT * FROM { TableName }");
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

        IBlacklistMobilenumber InitialResult;

        InitialResult = Find(Mobilenumber).Result;

        if (InitialResult.id != null) Result = true;
        else Result = false;

        return Task.FromResult(Result);
    }

    public Task<bool> Delete(IBlacklistMobilenumber Mobilenumber)
    {
        bool Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                dbConnection.Query($"DELETE FROM {TableName} WHERE mobilenumber=@mobilenumber", Mobilenumber);
                Result = true;
            }
            catch
            {
                Result = false;
                throw;
            }
        }

        return Task.FromResult(true);
    }

    public Task<bool> Update(IBlacklistMobilenumber Mobilenumber)
    {
        bool Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString)) 
        {
            try
            {
                dbConnection.Query($"UPDATE {TableName} SET mobilenumber=@mobilenumber WHERE id=@id", Mobilenumber);
                Result = true;
            }
            catch
            {
                Result = false;
                throw;
            }
        }        

        return Task.FromResult(Result);
    }

}
