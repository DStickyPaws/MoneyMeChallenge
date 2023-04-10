using RestAPIServer.Interface;
using System.Text.Json.Serialization;

namespace RestAPIServer.Models;

/**
    <summary>
        A set of a possible data record information, representing a blacklisted domain.
    </summary>
*/
public record BlacklistedDomain : IBlacklistedDomain
{
    /**
        <inheritdoc />
    */
    [JsonPropertyName("Id")]
    public long? Id { get;  set; }

    /**
        <inheritdoc />
    */
    [JsonPropertyName("EmailDomain")]
    public string EmailDomain { get; set; }

    /**
        <summary>
            The constructor of the blacklisted domain's possible data record.
        </summary>        
        <param name="EmailDomain">string. The domain to be blacklisted.</param>
        <param name="Id">[optional] long nullable. The Id of the supposed record.</param>
    */
    [JsonConstructor]
    public BlacklistedDomain(string EmailDomain, long? Id = null)
    {
        this.Id = Id;
        this.EmailDomain = EmailDomain;
    }
}
