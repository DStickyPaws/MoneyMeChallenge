using Dapper;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data;
using System.Data.SQLite;

namespace RestAPIServer.Engines;

/**
    <summary>
        An engine responsible for driving the Blacklisted Domain Logic Model.
    </summary>
*/
public class BlacklistedDomainEngine
{
    private const string TableName = "BlacklistedDomains";
    private const string Field1 = "Id";
    private const string Field2 = "EmailDomain";

    /**
        <summary>
            The application's configuration wrapped as interface.
        </summary>
    */
    private IConfiguration Configuration { get; set; }

    /**
        <summary>
            The ConnectionString used to connect to the database.
        </summary>
    */
    private string ConnectionString { get; set; }

    /**
        <summary>
            The Blacklisted Domain Engine's Constructor.
        </summary>
        <param name="Configuration">IConnfiguration. The Application's Configuration wrapped as an IConfiguration.</param>
    */
    public BlacklistedDomainEngine(IConfiguration Configuration)
    {
        this.Configuration = Configuration;
        ConnectionString = Utilities.GetConnectionString(this.Configuration).Result;
    }

    /**
        <summary>
            The method that creates an IBlacklistDomain.
        </summary>
        <param name="blacklistedDomain">BlacklistedDomain. A blacklisted domain record model.</param>
        <returns>IBlacklistedDomain. A data record representation of a blacklisted domain.</returns>
    */
    public Task<IBlacklistedDomain> Create(BlacklistedDomain blacklistedDomain) 
    {
        IBlacklistedDomain Result;

        Result = new BlacklistedDomain(blacklistedDomain.EmailDomain, blacklistedDomain.Id);

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that creates an IBlacklistDomain
        </summary>
        <param name="EmailDomain">string. The email domain that is possibly blacklisted.</param>
        <param name="Id">[Optional] long nullable. The Id representating the data record of a blacklisted domain.</param>
        <returns>IBlacklistedDomain. A data record representation of a blacklisted domain.</returns>
    */
    public Task<IBlacklistedDomain> Create(string EmailDomain, long? Id = null) 
    {
        IBlacklistedDomain Result;

        Result = new BlacklistedDomain(EmailDomain, Id);

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that validates if an IBlacklistedDomain is valid or not.
        </summary>
        <param name="blacklistedDomain">IBlacklistedDomain. The IBlacklistedDomain that is being validated.</param>
        <returns>bool. True for valid; False for invalid.</returns>
    */
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

    /**
        <summary>
            The method that determines if a provided IBlacklistedDomain is blacklisted  or not.
        </summary>
        <param name="blacklistedDomain">IBlacklistedDomain. The IBlacklistedDomain that is being checked.</param>
        <returns>bool. True for valid; False for invalid.</returns>
    */
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

    /**
        <summary>
            The method that saves the IBlacklistedDomain in the database.
        </summary>
        <param name="blacklistedDomain">IBlacklistedDomain. The IBlacklistedDomain that is to be saved in the database.</param>
        <returns>bool. True for success; False for fail.</returns>
    */
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
                        dbConnection.Query($"INSERT INTO {TableName} ({Field2}) VALUES (@emaildomain)", blacklistedDomain);
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

    /**
        <summary>
            The methods that searches for an IBlacklistedDomain in the database.
        </summary>
        <param name="blacklistedDomain">IBlacklistedDomain. The IBlacklistedDomain that is to be searched.</param>
        <returns>IBlacklistedDomain. The IBlacklistedDomain that is searched from the database. If the Id is null then the IBlacklistedDomain is not found within the database records</returns>
    */
    public Task<IBlacklistedDomain> Find(IBlacklistedDomain blacklistedDomain)
    {
        BlacklistedDomain? InitialResult;
        IBlacklistedDomain Result;            

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<BlacklistedDomain>($"SELECT {Field1}, {Field2} FROM { TableName } WHERE emaildomain=@emaildomain", blacklistedDomain).SingleOrDefault();
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

    /**
        <summary>
            The methods that searches for an IBlacklistedDomain in the database.
        </summary>
        <param name="id">long. The Id of the database record that is being searched for.</param>
        <returns>IBlacklistedDomain. The IBlacklistedDomain that is searched from the database. If the Id is null then the IBlacklistedDomain is not found within the database records</returns>
    */
    public Task<IBlacklistedDomain> Find(long id)
    {
        BlacklistedDomain? InitialResult;
        IBlacklistedDomain Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<BlacklistedDomain>($"SELECT {Field1}, {Field2} FROM {TableName} WHERE id=@id", id).SingleOrDefault();
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

    /**
        <summary>
            Obtains all the IBlacklistedDomain records from the database.
        </summary>
        <returns>IEnumerable<IBlacklistedDomain>. A collection of IBlacklistedDomains stored in the database.</returns>
    */
    public Task<IEnumerable<IBlacklistedDomain>> GetAllBlacklistedDomains()
    {
        IEnumerable<IBlacklistedDomain> Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            Result = dbConnection.Query<BlacklistedDomain>($"SELECT {Field1}, {Field2} FROM {TableName}");
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that updates the IBlacklistedDomain database record.
        </summary>
        <param name="blacklistedDomain">IBlacklistedDomain (Amalgamated). An amalagamated IBlacklisted domain in which the Id of the record is of the current value and the fields contains the new values.</param>
        <returns>bool. True for success; False for fail</returns>
    */
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

    /**
        <summary>
            The method that deletes the IBlacklistedDomain record from the database.
        </summary>
        <param name="blacklistedDomain">IBlacklistedDomain. The IBlacklistedDomain that is to be deleted from the database records.</param>
        <returns>bool. True for success; False for fail.</returns>
    */
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
