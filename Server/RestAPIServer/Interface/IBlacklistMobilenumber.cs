using System.Text.Json.Serialization;

namespace RestAPIServer.Interface;

/** 
 <summary>This is for the black listed mobile numbers.</summary>
*/
public interface IBlacklistMobilenumber
{
    /** 
        <summary> 
            The id of the blacklisted mobile number
        </summary>
        <remarks>
            int / Int32. The blacklisted mobile number id in the database.
        </remarks>
    */
    [JsonPropertyName("Id")]
    public int? id { get; }
    /** 
        <summary>
            The mobilenumber that is blacklisted.
        </summary>
        <remarks>
            string. The blackllisted mobilenumber.
        </remarks>
    */
    [JsonPropertyName("MobileNumber")]
    public string mobilenumber { get; }
}
