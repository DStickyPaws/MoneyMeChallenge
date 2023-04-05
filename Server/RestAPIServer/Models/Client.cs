using RestAPIServer.Interface;
using System.Text.Json.Serialization;

namespace RestAPIServer.Models;

public record Client : IClient
{
    [JsonPropertyName("Id")]
    public long? Id { get; set; }

    [JsonPropertyName("FirstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("LastName")]
    public string LastName { get; set; }

    [JsonPropertyName("DateOfBirth")]
    public string DateOfBirth { get; set; }

    [JsonConstructor]
    public Client(string FirstName, string LastName, string DateOfBirth, long? Id = null)
    {
        this.Id = Id;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.DateOfBirth = DateOfBirth;
    }
}
