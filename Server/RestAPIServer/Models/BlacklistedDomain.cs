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
    public long? id { get;  set; }

    /**
        <inheritdoc />
    */
    [JsonPropertyName("EmailDomain")]
    public string emaildomain { get; set; }

    /**
        <summary>
            The constructor of the blacklisted domain's possible data record.
        </summary>        
        <param name="EmailDomain">string. The domain to be blacklisted.</param>
        <param name="Id">[optional] long nullable. The Id of the supposed record.</param>
    */
    [JsonConstructor]
    public BlacklistedDomain(string emaildomain, long? Id = null)
    {
        this.id = id;
        this.emaildomain = emaildomain;
    }
}
