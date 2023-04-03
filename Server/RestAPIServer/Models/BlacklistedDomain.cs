using RestAPIServer.Interface;
using System.Text.Json.Serialization;

namespace RestAPIServer.Models;

public record BlacklistedDomain : IBlacklistedDomain
{
    public long? id { get; set; }

    public string emaildomains { get; private set; }

    [JsonConstructor]
    public BlacklistedDomain(string emaildomains)
    {
        this.emaildomains = emaildomains;
    }

    public BlacklistedDomain(long id, string emaildomains)
    {
        this.id = id;
        this.emaildomains = emaildomains;
    }

    public static Task<IBlacklistedDomain> Create(string emaildomains)
    {
        IBlacklistedDomain Result;

        Result = new BlacklistedDomain(emaildomains);

        return Task.FromResult(Result);
    }

    public static Task<IBlacklistedDomain> Create(long id, string emaildomains)
    {
        IBlacklistedDomain Result;

        Result = new BlacklistedDomain(id,emaildomains);

        return Task.FromResult(Result);
    }
}
