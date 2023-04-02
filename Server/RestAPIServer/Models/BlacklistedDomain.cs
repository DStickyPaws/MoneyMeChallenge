using RestAPIServer.Interface;

namespace RestAPIServer.Models;

public record BlacklistedDomain : IBlacklistedDomain
{
    public int? id { get; set; }

    public string emaildomains { get; private set; }

    public BlacklistedDomain(string emaildomains)
    {
        this.emaildomains = emaildomains;
    }

    public BlacklistedDomain(int id, string emaildomains)
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

    public static Task<IBlacklistedDomain> Create(int id, string emaildomains)
    {
        IBlacklistedDomain Result;

        Result = new BlacklistedDomain(id,emaildomains);

        return Task.FromResult(Result);
    }
}
