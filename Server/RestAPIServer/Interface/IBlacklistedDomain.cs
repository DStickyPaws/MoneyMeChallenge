using System.Text.Json.Serialization;

namespace RestAPIServer.Interface;

/** 
    <summary>
        A data record representation of a blacklisted domain.
    </summary>
*/
public interface IBlacklistedDomain
{
    /** 
        <summary>
            The Id representating the data record of a blacklisted domain.
        </summary>
    */
    [JsonPropertyName("Id")]
    public long? Id { get; set; }
    /** 
        <summary>
            The domain that is supposed to be blacklisted.
        </summary>
    */
    [JsonPropertyName("EmailDomain")]
    public string EmailDomain { get; set; }
}
