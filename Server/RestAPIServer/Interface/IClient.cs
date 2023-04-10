using System.Text.Json.Serialization;

namespace RestAPIServer.Interface;

/**
    <summary>
        A data record representation of a client.
    </summary>
*/
public interface IClient
{
    /**
        <summary>
            The ID representing the data record of a client.
        </summary>
    */
    [JsonPropertyName("Id")]
    long? Id { get; set; }
    /**
        <summary>
            The First Name of the Client.
        </summary>
    */
    [JsonPropertyName("FirstName")]
    string FirstName { get; set; }
    /**
        <summary>
            The Last Name of the Client.
        </summary>
    */
    [JsonPropertyName("LastName")]
    string LastName { get; set; }
    /**
        <summary>
            The client's date of birth.
        </summary>
    */
    [JsonPropertyName("DateOfBirth")]
    string DateOfBirth { get; set; }
}
