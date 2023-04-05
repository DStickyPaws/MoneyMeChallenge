using RestAPIServer.Interface;
using RestAPIServer.Models;

namespace RestAPIServer.Engines;

public class ClientEngine
{
    /**
        <summary>
            Creates an IClient based on Client.
        </summary>
        <param name="client">Record. The Client information. This is either made using JSON or using the backend.</param>
        <returns>
            IClient.
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
            IClient.
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
        
        </summary>
        <param name="Client"></param>
        <returns></returns>
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
    private Task<bool> IsValidId(long? Id) { return Task.FromResult(true); }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="FirstName"></param>
    /// <returns></returns>
    private Task<bool> IsValidFirstName(string FirstName) { return Task.FromResult(true); }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="LastName"></param>
    /// <returns></returns>
    private Task<bool> IsValidLastName(string LastName) { return Task.FromResult(true); }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="DateOfBirth"></param>
    /// <returns></returns>
    private Task<bool> IsValidDateOfBirth(string DateOfBirth) { return Task.FromResult(true); }
}
