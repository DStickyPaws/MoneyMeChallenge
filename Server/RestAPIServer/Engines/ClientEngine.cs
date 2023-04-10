using Dapper;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data;
using System.Data.SQLite;

namespace RestAPIServer.Engines;

/**
    <summary>
        The engine class that drive's the Client Model.
    </summary>
*/
public class ClientEngine
{
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
            The name of the table associated with the Client Model.
        </summary>
    */
    private const string TableName = "Clients";
    /**
        <summary>
            The Engine's constructor.
        </summary>
        <param name="Configuration">IConfiguration. The Application's Configuration wrapped as IConfiguration interface.</param>
    */
    public ClientEngine(IConfiguration Configuration)
    {
        this.Configuration = Configuration;
        ConnectionString = GetConnectionString().Result;
    }

    /**
        <summary>
            Obtains the connection string stored within the application's configuration file.
        </summary>
        <returns>
            string. The connection string stored within the application's configuration file.
        </returns>
    */
    private Task<string> GetConnectionString()
    {                
        string? ConnectionString;
        string Result;

        ConnectionString = Configuration.GetConnectionString("sqlLite");

        Result = ConnectionString ?? string.Empty;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Creates an IClient based on Client.
        </summary>
        <param name="client">Record. The Client information. This is either made using JSON or using the backend.</param>
        <returns>
            IClient. Representing the Client Information
        </returns>
    */
    public Task<IClient> Create(Client client)
    {
        IClient Result;

        Result = new Client(client.FirstName,client.LastName,client.DateOfBirth, client.Id);

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Creates an IClient based on parameters provided.
        </summary>
        <param name="FirstName">String. The client's supposed FirstName</param>
        <param name="LastName">String. The client's supposed LastName</param>
        <param name="DateOfBirth">String. The client's supposed Date of Birth</param>
        <param name="Id">Optional. Long. </param>
        <returns>
            IClient. Representing the Client Information.
        </returns>
    */
    public Task<IClient> Create(string FirstName, string LastName, string DateOfBirth, long? Id = null)
    {
        IClient Result;

        Result = new Client(FirstName, LastName, DateOfBirth, Id);

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Validates a client's Id value.
        </summary>
        <param name="Id">Nullable Long. The Client's Id</param>
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
            Validates a client's FirstName.
        </summary>
        <param name="FirstName">String. The Client's supposed FirstName.</param>
        <remarks>
            Hey! You might be thinking that this is weird right? Well Yeah it is.
            But there is a reason for it, some person, natural person actually possess weird names.
            Some of the examples are Jon Version 2.0 which is an actual first name, given to a child
            by his father in US Jon Blake Cusack 2.0. chances are you might have heart of the name
            Elon Musk, the Tesla CEO. He has a kid named X Æ A-12 Musk. Get the point? So long as it
            is not empty its valid.
        </remarks>
        <returns>
            Boolean. True for Valid; False for Invalid
        </returns>
    */
    private Task<bool> IsValidFirstName(string FirstName) 
    {
        bool Result;

        if (string.IsNullOrWhiteSpace(FirstName)) Result = false;
        else Result = true;

        return Task.FromResult(Result); 
    }
    /**
        <summary>
            Validates a client's LastName.
        </summary>
        <param name="LastName">String. The Client's Supposed LastName.</param>
        <remarks>
            Okay I know you forgave me for the FirstName thing and this is the the LastName. Should be normal right?
            Still have you even hear about Dr. Vivian U? No? https://astro.ucr.edu/members/postdocs/vivianu/ check this.
            Not Convinced? Ever hear of the Chinese UI/UX Designing Allie E? No? https://www.quora.com/profile/Allie-E-2
            So yeah. As long as its not empty its valid. P.S. There are a lot of people with a single character / special
            character LastName. Just because you don't hear about it, it doesn't mean it is not true.
        </remarks>
        <returns>
            Boolean. True for Valid; False for Invalid
        </returns>
    */
    private Task<bool> IsValidLastName(string LastName) 
    {
        bool Result;

        if (string.IsNullOrWhiteSpace(LastName)) Result = false;
        else Result = true;

        return Task.FromResult(Result); 
    }

    /**
        <summary>
            Validates a client's Birthdate.
        </summary>
        <param name="DateOfBirth">DateOnly. The client's supposed birthdate.</param>
        <returns>
            Boolean. True for Valid; False for Invalid
        </returns>
    */
    private Task<bool> IsValidDateOfBirth(string DateOfBirth) 
    { 
        bool Result;
        DateOnly Birthdate;

        Result = DateOnly.TryParse(DateOfBirth, out Birthdate);       

        return Task.FromResult(Result); 
    }

    /**
        <summary>
            Validates whether a client is valid or not.
        </summary>
        <param name="Client">IClient. A client model, record, or class that is to be validated. </param>
        <returns>
            Boolean. True for Valid; False for Invalid
        </returns>
    */
    public Task<bool> IsValid(IClient Client)
    {
        bool Result, isValidId, isValidFirstName, isValidLastName, isValidDateOfBirth;

        isValidId = IsValidId(Client.Id).Result;
        isValidFirstName = IsValidFirstName(Client.FirstName).Result;
        isValidLastName = IsValidLastName(Client.LastName).Result;
        isValidDateOfBirth = IsValidDateOfBirth(Client.DateOfBirth).Result;

        Result = isValidId && isValidFirstName && isValidLastName && isValidDateOfBirth;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Finds a Client  in the database using the record Id.
        </summary>
        <param name="Id">Long. The Record Id of the client.</param>
        <returns>
            Nullable IClient. The record representing the client, or null if not found.
        </returns>
    */
    public Task<IClient?> Find(long Id) 
    {
        IClient? Result;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                Result = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName} WHERE Id=@Id", Id).SingleOrDefault();
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
            Finds a Client  in the database using the stored FirstName and LastName.
        </summary>
        <param name="FirstName">String. The FirstName of the Client.</param>
        <param name="LastName">String. The LastName of the Client.</param>
        <returns>
            Nullable IClient. The record representing the client, or null if not found.
        </returns>
    */
    public Task<IClient?> Find(string FirstName, string LastName) 
    {
        IClient? Result;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                Result = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName", new { FirstName, LastName }).SingleOrDefault();
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
            Finds a Client  in the database using the stored FirstName, LastName and, Date of Birth.
        </summary>
        <param name="FirstName">String. The FirstName of the Client.</param>
        <param name="LastName">String. The LastName of the Client.</param>
        <param name="DateOfBirth">String. The Date of Birth of the Client.</param>
        <returns>
            Nullable IClient. The record representing the client, or null if not found.
        </returns>
    */
    public Task<IClient?> Find(string FirstName, string LastName, string DateOfBirth) 
    {
        IClient? Result;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                Result = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName AND DateOfBirth=@DateOfBirth", new { FirstName, LastName, DateOfBirth }).SingleOrDefault();
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
            Finds a Client  in the database using the stored FirstName and LastName.
        </summary>
        <param name="Client">IClient. Client Record representing the Client Record you wish to find.</param>
        <returns>
            Nullable IClient. The record representing the client, or null if not found.
        </returns>
    */
    public Task<IClient?> Find(IClient Client) 
    {
        IClient? Result;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                Result = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName", Client).SingleOrDefault();
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
            Gets all stored client record.
        </summary>
        <returns>
            IEnumerable<IClient>. A collection of client reocords. 
        </returns>
    */
    public Task<IEnumerable<IClient>> GetAll()
    {
        IEnumerable<IClient> Result;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                Result = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName}");
            }
            catch
            {
                Result = new List<IClient>();
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Determines if the Client records exist or not.
        </summary>
        <param name="Id">Long. The Id of the record.</param>
        <returns>
            Boolean. True for Existing; False for Not Existing
        </returns>
    */
    public Task<bool> IsExisting(long Id)
    {
        bool Result;
        IClient? InitialResult;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString)) 
        {
            try
            { 
                InitialResult = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName} WHERE Id=@Id", Id).SingleOrDefault();
                if (InitialResult == null) Result = false;
                else Result = true;
            }
            catch
            {
                Result = false;
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Determines if the Client records exist or not.
        </summary>
        <param name="FirstName">String. The Client's FirstName</param>
        <param name="LastName">String. The Client's LastName</param>
        <returns>
            Boolean. True for Existing; False for Not Existing
        </returns>
    */
    public Task<bool> IsExisting(string FirstName, string LastName)
    {
        bool Result;
        IClient? InitialResult;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName", new { FirstName, LastName }).SingleOrDefault();

                if (InitialResult == null) Result = false;
                else Result = true;

            }
            catch 
            {
                Result = false;
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Determines if the Client records exist or not.
        </summary>
        <param name="FirstName">String. The Client's FirstName</param>
        <param name="LastName">String. The Client's LastName</param>
        <param name="DateOfBirth">String. The Client's Date of Birth</param>
        <returns>
            Boolean. True for Existing; False for Not Existing
        </returns>
    */
    public Task<bool> IsExisting(string FirstName, string LastName, string DateOfBirth)
    {
        bool Result;
        IClient? InitialResult;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName AND DateOfBirth=@DateOfBirth", new { FirstName, LastName, DateOfBirth }).SingleOrDefault();

                if (InitialResult == null) Result = false;
                else Result = true;

            }
            catch
            {
                Result = false;
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Determines if the Client records exist or not.
        </summary>
        <param name="Client">IClient. A representation of the client's record</param>
        <returns>
            Boolean. True for Existing; False for Not Existing
        </returns>
    */
    public Task<bool> IsExisting(IClient Client)
    {
        bool Result;
        IClient? InitialResult;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                InitialResult = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName", Client).SingleOrDefault();

                if (InitialResult == null) Result = false;
                else Result = true;

            }
            catch
            {
                Result = false;
                throw;
            }
        }

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Save the client information as a database record.
        </summary>
        <param name="Client">IClient. A Client Representative Record.</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Save(IClient Client)
    {        
        bool Result, isExisting, isValid;

        isValid = IsValid(Client).Result;

        if (isValid)
        {
            isExisting = IsExisting(Client).Result;
            if (isExisting) Result = false;
            else
            {
                using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"INSERT INTO { TableName } (FirstName, LastName, DateOfBirth) VALUES (@FirstName, @LastName, @DateOfBirth) ", Client);
                        Result = true;
                    }
                    catch
                    {
                        Result = false;
                        throw;
                    }
                }
            }
        }
        else Result = false;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Save the client information as a database record.
        </summary>
        <param name="FirstName">String. Client's FirstName</param>
        <param name="LastName">String. Client's LastName</param>
        <param name="DateOfBirth">String. Client's Date of Birth</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Save(string FirstName, string LastName, string DateOfBirth)
    {
        bool Result, isExisting, isValid;
        IClient ClientRepresentativeRecord;

        ClientRepresentativeRecord = Create(FirstName, LastName, DateOfBirth).Result;

        isValid = IsValid(ClientRepresentativeRecord).Result;

        if (isValid)
        {
            isExisting = IsExisting(ClientRepresentativeRecord).Result;
            if (isExisting) Result = false;
            else
            {
                using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"INSERT INTO {TableName} (FirstName, LastName, DateOfBirth) VALUES (@FirstName, @LastName, @DateOfBirth) ", ClientRepresentativeRecord);
                        Result = true;
                    }
                    catch
                    {
                        Result = false;
                        throw;
                    }
                }
            }
        }
        else Result = false;

        return Task.FromResult(Result);
    }

    /**
        <summary>
            Updates a client record based on newly provided values.
        </summary>
        <param name="CurrentClient">IClient. The current client representative record.</param>
        <param name="NewClient">IClient. The new client representative record</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Update(IClient CurrentClient, IClient NewClient )
    {
        bool Result, isExisting, isValid;

        isValid = IsValid(NewClient).Result;
        if (isValid)
        {
            isExisting= IsExisting(CurrentClient).Result;
            if (isExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"UPDATE { TableName } SET FirstName=@FirstName, LastName=@LastName DateOfBirth=@DateOfBirth WHERE Id={CurrentClient.Id}", NewClient);
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
            Updates a client record based on newly provided values.
        </summary>
        <param name="Client">IClient. Amalgamation type parameter in which it combines new data and current data into one record.</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Update(IClient Client) 
    {
        bool Result, isExisting, isValid;
        IClient ClientRepresentativeRecord;

        ClientRepresentativeRecord = Create(Client.FirstName, Client.LastName, Client.DateOfBirth).Result;

        isValid = IsValid(ClientRepresentativeRecord).Result;

        if (isValid)
        {
            isExisting = IsExisting(ClientRepresentativeRecord).Result;
            if (isExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"UPDATE { TableName } SET FirstName=@FirstName, LastName=@LastName DateOfBirth=@DateOfBirth WHERE Id={Client.Id}", Client);
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
            Updates a client record based on newly provided values.
        </summary>
        <param name="Id">Long. The current Id of the record.</param>
        <param name="FirstName">String. The new FirstName value to be stored as record.</param>
        <param name="LastName">String. The new LastName value to be stored as record.</param>
        <param name="DateOfBirth">String. The new Date of Birth value to be stored as a record.</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Update(long Id, string FirstName, string LastName, string DateOfBirth) 
    {
        bool Result, isExisting, isValid;
        IClient ClientRepresentativeRecord;

        ClientRepresentativeRecord = Create(FirstName, LastName, DateOfBirth).Result;

        isValid = IsValid(ClientRepresentativeRecord).Result;
        if (isValid)
        {
            isExisting = IsExisting(ClientRepresentativeRecord).Result;
            if (isExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"UPDATE {TableName} SET FirstName=@FirstName, LastName=@LastName DateOfBirth=@DateOfBirth WHERE Id={Id}", ClientRepresentativeRecord);
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
            Updates a client record based on newly provided values.
        </summary>
        <param name="CurrentFirstName">String. The current FirstName value that is stored as record.</param>
        <param name="CurrentLastName">String. The current LastName value that is stored as record.</param>
        <param name="CurrentDateOfBirth">String. The current Date of Birth value that is stored as a record.</param>
        <param name="NewFirstName">String. The new FirstName value to be stored as record.</param>
        <param name="NewLastName">String. The new LastName value to be stored as record.</param>
        <param name="NewDateOfBirth">String. The new Date of Birth value to be stored as a record.</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Update(string CurrentFirstName, string CurrentLastName, string CurrentDateOfBirth, string NewFirstName, string NewLastName, string NewDateOfBirth)
    {
        bool Result, isCurrentExisting, isNewExisting, isCurrentValid, isNewValid, isValid;
        IClient NewClientRepresentativeRecord, CurrentClientRepresentativeRecord;

        CurrentClientRepresentativeRecord = Create(CurrentFirstName, CurrentLastName, CurrentDateOfBirth).Result;
        NewClientRepresentativeRecord  = Create(NewFirstName, NewLastName, NewDateOfBirth).Result;

        isNewValid = IsValid(NewClientRepresentativeRecord).Result;
        isCurrentValid = IsValid(CurrentClientRepresentativeRecord).Result;

        isValid = isNewValid && isCurrentValid;

        if (isValid)
        {
            isCurrentExisting = IsExisting(CurrentClientRepresentativeRecord).Result;
            isNewExisting = IsExisting(NewClientRepresentativeRecord).Result;

            if (!isNewExisting && isCurrentExisting)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
                {
                    try
                    {
                        dbConnection.Query($"UPDATE {TableName} SET FirstName=@FirstName, LastName=@LastName DateOfBirth=@DateOfBirth WHERE Id={CurrentClientRepresentativeRecord.Id}", NewClientRepresentativeRecord);
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
           Deletes a client record from the storage.
        </summary>
        <param name="Id">Long. The Id of the record.</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Delete(long Id) 
    { 
        bool Result, isExisting;

        isExisting = IsExisting(Id).Result;
        if (isExisting)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Query($"DELETE FROM {TableName} WHERE Id=@Id", Id);
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

    /**
        <summary>
            Deletes a client record from the storage.
        </summary>
        <param name="FirstName">String. Client's FirstName</param>
        <param name="LastName">String. Client's LastName</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Delete(string FirstName, string LastName) 
    {
        bool Result, isExisting;

        isExisting = IsExisting(FirstName, LastName).Result;
        if (isExisting)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Query($"DELETE FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName", new { FirstName, LastName });
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

    /**
        <summary>
            Deletes a client record from the storage.
        </summary>
        <param name="FirstName">String. Client's FirstName</param>
        <param name="LastName">String. Client's LastName</param>
        <param name="DateOfBirth">String. Client's Date of Birth</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Delete(string FirstName, string LastName, string DateOfBirth) 
    {
        bool Result, isExisting;

        isExisting = IsExisting(FirstName, LastName, DateOfBirth).Result;
        if (isExisting)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Query($"DELETE FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName AND DateOfBirth=@DateOfBirth", new { FirstName, LastName, DateOfBirth });
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

    /**
        <summary>
            Deletes a client record from the storage.
        </summary>
        <param name="Client">IClient. A Client Representative Record.</param>
        <returns>
            Boolean. True for successful operation ; False for unsuccessful operation
        </returns>
    */
    public Task<bool> Delete(IClient Client) 
    {
        bool Result, isExisting;

        isExisting = IsExisting(Client).Result;
        if (isExisting)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Query($"DELETE FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName", Client);
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
