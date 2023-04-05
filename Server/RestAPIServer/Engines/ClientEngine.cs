using Dapper;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data;
using System.Data.SQLite;

namespace RestAPIServer.Engines;

public class ClientEngine
{
    private IConfiguration Configuration { get; set; }
    private string ConnectionString { get; set; }
    
    private const string TableName = "Clients";

    public ClientEngine(IConfiguration Configuration)
    {
        this.Configuration = Configuration;
        ConnectionString = GetConnectionString().Result;
    }

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

        return Task.FromResult(true); 
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <param name="DateOfBirth"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Client"></param>
    /// <returns></returns>
    public Task<IClient?> Find(IClient Client) 
    {
        IClient? Result;

        using (IDbConnection dbConnection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                Result = dbConnection.Query<IClient>($"SELECT Id, FirstName, LastName, DateOfBirth FROM {TableName} WHERE FirstName=@FirstName AND LastName=@LastName AND DateOfBirth=@DateOfBirth", Client).SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        return Task.FromResult(Result);
    }
}

