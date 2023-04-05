using RestAPIServer.Interface;
using System.Text.Json.Serialization;

namespace RestAPIServer.Models;

public record BlacklistedDomain : IBlacklistedDomain
{
    [JsonPropertyName("Id")]
    public long? id { get; private set; }

    [JsonPropertyName("EmailDomain")]
    public string emaildomain { get; set; }

    [JsonConstructor]
    public BlacklistedDomain(string emaildomain)
    {
        this.emaildomain = emaildomain;
    }

    public BlacklistedDomain(long id, string emaildomain)
    {
        this.id = id;
        this.emaildomain = emaildomain;
    }

    public static Task<IBlacklistedDomain> Create(string emaildomain)
    {
        IBlacklistedDomain Result;

        Result = new BlacklistedDomain(emaildomain);

        return Task.FromResult(Result);
    }

    public static Task<IBlacklistedDomain> Create(long id, string emaildomain)
    {
        IBlacklistedDomain Result;

        Result = new BlacklistedDomain(id,emaildomain);

        return Task.FromResult(Result);
    }
}
