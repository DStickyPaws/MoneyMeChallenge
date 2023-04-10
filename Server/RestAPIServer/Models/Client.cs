using RestAPIServer.Interface;
using System.Text.Json.Serialization;

namespace RestAPIServer.Models;

/**
    <summary>
        A set of a possible data record information, representing a client.
    </summary>
*/
public record Client : IClient
{

    /**
        <inheritdoc/>
    */
    [JsonPropertyName("Id")]
    public long? Id { get; set; }

    /**
        <inheritdoc/>
    */
    [JsonPropertyName("FirstName")]
    public string FirstName { get; set; }

    /**
        <inheritdoc/>
    */
    [JsonPropertyName("LastName")]
    public string LastName { get; set; }

    /**
        <inheritdoc/>
    */
    [JsonPropertyName("DateOfBirth")]
    public string DateOfBirth { get; set; }

    /**
        <summary>
            The constructor of the client's possible data record.
        </summary>
        <param name="FirstName">string. The client's supposed First Name</param>
        <param name="LastName">string. The client's supposed Last Name</param>
        <param name="DateOfBirth">string. The client's supposed Date of Birth</param>
        <param name="Id">[optional] long nullable. The Id of the supposed record.</param>
    */
    [JsonConstructor]
    public Client(string FirstName, string LastName, string DateOfBirth, long? Id = null)
    {
        this.Id = Id;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.DateOfBirth = DateOfBirth;
    }
}
