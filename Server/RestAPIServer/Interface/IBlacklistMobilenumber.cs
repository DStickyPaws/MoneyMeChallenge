using System.Text.Json.Serialization;

namespace RestAPIServer.Interface;

/** 
 <summary>
    A data record representation of a blacklisted mobile number.
</summary>
*/
public interface IBlacklistMobilenumber
{
    /** 
        <summary> 
            The Id representating the data record of a blacklisted mobilenumber.
        </summary>
    */
    [JsonPropertyName("Id")]
    public long? Id { get; set; }
    /** 
        <summary>
            The mobile number that is supposed to be blacklisted.
        </summary>
    */
    [JsonPropertyName("MobileNumber")]
    public string MobileNumber { get; set; }
}
