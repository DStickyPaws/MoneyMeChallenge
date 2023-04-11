using Dapper;
using System.Data.SQLite;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data;

namespace RestAPIServer.Engines;

/**
    <summary>
        An engine responsible for driving the Blacklisted Mobile Number Logic Model.
    </summary>
*/
internal class BlacklistedMobileNumberEngine
{
    private const string TableName = "BlacklistedMobileNumbers";
    private const string Field1 = "Id";
    private const string Field2 = "MobileNumber";

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
            The Blacklisted Mobile Number Engine's Constructor.
        </summary>
        <param name="Configuration">IConnfiguration. The Application's Configuration wrapped as an IConfiguration.</param>
    */
    public BlacklistedMobileNumberEngine(IConfiguration Configuration)
    {
        this.Configuration = Configuration;
        ConnectionString = ConnectionString = Utilities.GetConnectionString(this.Configuration).Result;
    }

    /**
        <summary>
            The method that creates an IBlacklistMobilenumber.
        </summary>
        <param name="blacklistedMobileNumber">BlacklistedMobileNumber. A blacklisted mobile number record model.</param>
        <returns>IBlacklistMobileNumber. A data representation of a blacklisted mobile number.</returns>
    */
    public Task<IBlacklistMobileNumber> Create(BlacklistedMobileNumber blacklistedMobileNumber) 
    {
        IBlacklistMobileNumber Result;

        Result = new BlacklistedMobileNumber(blacklistedMobileNumber.MobileNumber, blacklistedMobileNumber.Id);

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that creates an IBlacklistMobilenumber.
        </summary>
        <param name="mobileNumber">string. The mobile number that is possibly blacklisted.</param>
        <returns>IBlacklistMobileNumber. A data representation of a blacklisted mobile number.</returns>
    */
    public Task<IBlacklistMobileNumber> Create(string mobileNumber) 
    {
        IBlacklistMobileNumber Result;

        Result = new BlacklistedMobileNumber(mobileNumber);

        return Task.FromResult(Result);
    }

    /**
    <summary>
        Validates a mobile number's Id value.
    </summary>
    <param name="Id">Nullable Long. The mobile number's Id</param>
    <returns>
        Boolean. True for Valid; False for Invalid
    </returns>
*/
    private Task<bool> IsValidId(long? Id)
    {
        bool Result;

        if (Id == null) Result = true;
        else Result = true;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that checks if a provided mobile number is valid or not.
        </summary>
        <param name="mobileNumber">string. The mobile number to be validated.</param>
        <returns>bool. True for valid ; False for Invalid.</returns>
    */
    public Task<bool> IsValidMobileNumber(string mobileNumber)
    {
        bool Result;

        Result = true;

        if (mobileNumber.Trim().Length <= 9) Result = false;
        else
        {
            foreach (char c in mobileNumber)
            {
                if (!char.IsNumber(c))
                {
                    if (c != '+') Result = false; break;
                }
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that checks if an IBlacklistedMobileNumber is valid or not.
        </summary>
        <param name="blacklistMobileNumber"></param>
        <returns>bool. True for valid; False for invalid.</returns>
    */
    public Task<bool> IsValid(IBlacklistMobileNumber blacklistMobileNumber)
    {
        bool Result, isValidMobileNumber, isValidId;

        isValidId = IsValidId(blacklistMobileNumber.Id).Result;
        isValidMobileNumber = IsValidMobileNumber(blacklistMobileNumber.MobileNumber).Result;

        Result = isValidId && isValidMobileNumber;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that saves the IBlacklistedMobileNumber in the database records.
        </summary>
        <param name="blacklistMobileNumber">IBlacklistMobileNumber. The IBlacklistedMobileNumber to be saved in the database records.</param>
        <returns>bool. True for success; False for fail.</returns>
    */
    public Task<bool> Save(IBlacklistMobileNumber blacklistMobileNumber)
    {
        bool Result, isValid, isExisting;

        isValid = IsValid(blacklistMobileNumber).Result;

        isExisting = IsExisting(blacklistMobileNumber).Result;

        if (isValid)
        {
            if (!isExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                {
                    try
                    {
                        dbConnection.Query<BlacklistedMobileNumber>($"INSERT INTO {TableName} ({Field2}) VALUES (@mobilenumber)", blacklistMobileNumber);
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
            The method that searches the database record based on the provided IBlacklistedMobileNumber.
        </summary>
        <param name="blacklistMobileNumber">IBlacklistMobileNumber. The IBlacklistMobileNumber to be searched for in the database records.</param>
        <returns>Nullable IBlacklistMobileNumber. The database record matching the provided parameter if null it does not exists on the database records.</returns>
    */
    public Task<IBlacklistMobileNumber?> Find(IBlacklistMobileNumber blacklistMobileNumber)
    {
        IBlacklistMobileNumber? Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                Result = dbConnection.Query<BlacklistedMobileNumber>($"SELECT {Field1}, {Field2} FROM {TableName} WHERE {Field2}=@MobileNumber AND {Field1}=@Id", blacklistMobileNumber).SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that searches the database record based on the provided IBlacklistedMobileNumber.
        </summary>
        <param name="Id">long. The record Id that is to be searched for within the database records.</param>
        <returns>Nullable IBlacklistMobileNumber. The database record matching the provided parameter if null it does not exists on the database records.</returns>
    */
    public Task<IBlacklistMobileNumber?> Find(long Id)
    {
        IBlacklistMobileNumber? Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                Result = dbConnection.Query<BlacklistedMobileNumber>($"SELECT {Field1}, {Field2} FROM {TableName} WHERE {Field1}={Id}").SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that searches the database record based on the provided IBlacklistedMobileNumber.
        </summary>
        <param name="mobileNumber">string. The mobile number that is used to search for within the database records.</param>
        <returns>Nullable IBlacklistMobileNumber. The database record matching the provided parameter if null it does not exists on the database records.</returns>
    */
    public Task<IBlacklistMobileNumber?> Find(string mobileNumber)
    {
        IBlacklistMobileNumber? Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                Result = dbConnection.Query<BlacklistedMobileNumber>($"SELECT {Field1}, {Field2} FROM {TableName} WHERE {Field2}={mobileNumber}").SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that determines whether a blacklisted mobilenumber is stored in the database records or not.
        </summary>
        <param name="blacklistMobilenumber">IBlacklistMobileNumber. The IIBlacklistMobileNumber to be checked if existing within the database records or not.</param>
        <returns>bool. True for existing; False for not existing.</returns>
    */
    public Task<bool> IsExisting(IBlacklistMobileNumber blacklistMobileNumber) 
    { 
        bool Result;
        IBlacklistMobileNumber? InitialResult;

        InitialResult = Find(blacklistMobileNumber).Result;

        if (InitialResult == null) Result = false;
        else Result = true;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that determines whether a blacklisted mobilenumber is stored in the database records or not.
        </summary>
        <param name="Id">long. The Id of the database record that is being searched for.</param>
        <returns>bool. True for existing; False for not existing.</returns>
    */
    public Task<bool> IsExisting(long Id) 
    {
        bool Result;
        IBlacklistMobileNumber? InitialResult;

        InitialResult = Find(Id).Result;

        if (InitialResult == null) Result = false;
        else Result = true;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that determines whether a blacklisted mobilenumber is stored in the database records or not.
        </summary>
        <param name="mobileNumber">string. The mobile number that is being searched for in the database records.</param>
        <returns>bool. True for existing; False for not existing.</returns>
    */
    public Task<bool> IsExisting(string mobileNumber) 
    {
        bool Result;
        IBlacklistMobileNumber? InitialResult;

        InitialResult = Find(mobileNumber).Result;

        if (InitialResult == null) Result = false;
        else Result = true;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that retrieves all IBlacklistMobileNumber 
        </summary>
        <returns>IEnumerable<IBlacklistMobileNumber>. A list of IBlacklistMobileNumbers stored in the database records.</returns>
    */
    public Task<IEnumerable<IBlacklistMobileNumber>> GetAllBlackListedMobileNumber()
    {
        IEnumerable<IBlacklistMobileNumber> Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                Result = (IEnumerable<BlacklistedMobileNumber>)dbConnection.Query<BlacklistedMobileNumber>($"SELECT {Field1}, {Field2} FROM { TableName }");
            }
            catch
            {
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that identifies if a provided IBlacklistMobileNumber is blacklisted or not.
        </summary>
        <param name="blacklistMobileNumber">IBlacklistMobileNumber. The IBlacklistMobileNumber that is to be checked against the datbase. </param>
        <returns>bool. True for is black listed; False for is not blacklisted.</returns>
    */
    public Task<bool> IsBlacklisted(IBlacklistMobileNumber blacklistMobileNumber)
    {
        bool Result;

        IBlacklistMobileNumber? InitialResult;

        InitialResult = Find(blacklistMobileNumber).Result;

        if (InitialResult != null) Result = true;
        else Result = false;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that identifies if a provided IBlacklistMobileNumber is blacklisted or not.
        </summary>
        <param name="mobileNumber">string. The mobile number that is to be checked against the datbase. </param>
        <returns>bool. True for is black listed; False for is not blacklisted.</returns>
    */
    public Task<bool> IsBlacklisted(string mobileNumber)
    {
        bool Result;

        IBlacklistMobileNumber? InitialResult;

        InitialResult = Find(mobileNumber).Result;

        if (InitialResult != null) Result = true;
        else Result = false;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            The method that updates the database records.
        </summary>
        <param name="blacklistMobileNumber">IBlacklistMobileNumber</param>
        <returns>bool. True for success; False for fail.</returns>
    */
    public Task<bool> Update(IBlacklistMobileNumber blacklistMobileNumber)
    {
        bool Result, isValid, isExisting;
        long RecordId;

        isValid = IsValid(blacklistMobileNumber).Result;

        if (isValid)
        {
            if (blacklistMobileNumber.Id.HasValue) RecordId = blacklistMobileNumber.Id.Value;
            else RecordId = 0;

            if (RecordId != 0) isExisting = IsExisting(RecordId).Result;
            else isExisting = false;

            if (isExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"UPDATE {TableName} SET {Field2}=@MobileNumber WHERE {Field1}=@Id", blacklistMobileNumber);
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

    // ToDo : Fill this up
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newBlacklistMobileNumber"></param>
    /// <param name="currentBlacklistMobileNumber"></param>
    /// <returns></returns>
    public Task<bool> Update(IBlacklistMobileNumber newBlacklistMobileNumber, IBlacklistMobileNumber currentBlacklistMobileNumber)
    {
        bool Result, isValid, isNewValid, isCurrentValid, isExisting;

        isNewValid = IsValid(newBlacklistMobileNumber).Result;
        isCurrentValid = IsValid(currentBlacklistMobileNumber).Result;

        isValid = isNewValid && isCurrentValid;

        if (isValid)
        {
            isExisting = IsExisting(currentBlacklistMobileNumber).Result;
            if (isExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"UPDATE {TableName} SET {Field2}=@MobileNumber WHERE {Field1}={currentBlacklistMobileNumber.Id}", newBlacklistMobileNumber);
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

    // ToDo : Fill this up
    /// <summary>
    ///  
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="MobileNumber"></param>
    /// <returns></returns>
    public Task<bool> Update(long Id, string MobileNumber)
    {
        bool Result, isValid, isExisting;
        IBlacklistMobileNumber InitialResult;

        InitialResult = Create(MobileNumber).Result;

        isValid = IsValid(InitialResult).Result;

        if (isValid)
        {
            isExisting = IsExisting(Id).Result;

            if (isExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"UPDATE {TableName} SET {Field2}=@MobileNumber WHERE {Field1}={Id}", InitialResult);
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

    // ToDo : Fill this up
    /**
        <summary>
    
        </summary>
        <param name="Mobilenumber"></param>
        <returns></returns>
    */
    public Task<bool> Delete(IBlacklistMobileNumber blacklistMobileNumber)
    {
        bool Result, isValid, isExisting;

        isValid = IsValid(blacklistMobileNumber).Result;

        if (isValid)
        {
            isExisting = IsExisting(blacklistMobileNumber).Result;
            if (isExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"DELETE FROM {TableName} WHERE {Field2}=@MobileNumber AND {Field1}=@Id", blacklistMobileNumber);
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

    // ToDo : Fill this up
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<bool> Delete(long Id)
    {
        bool Result, isExisting;

        isExisting = IsExisting(Id).Result;

        if (isExisting)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
            {
                try
                {
                    dbConnection.Query($"DELETE FROM {TableName} WHERE {Field1}=@Id", Id);
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

    // ToDo : Fill this up
    /// <summary>
    /// 
    /// </summary>
    /// <param name="MobileNumber"></param>
    /// <returns></returns>
    public Task<bool> Delete(string MobileNumber)
    {
        bool Result, isExisting;

        isExisting = IsExisting(MobileNumber).Result;

        if (isExisting)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
            {
                try
                {
                    dbConnection.Query($"DELETE FROM {TableName} WHERE {Field2}=@MobileNumber", MobileNumber);
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
}
