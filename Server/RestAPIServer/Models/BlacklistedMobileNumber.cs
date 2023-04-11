using RestAPIServer.Interface;
using System.Text.Json.Serialization;

namespace RestAPIServer.Models;

/** 
    <summary>
        A set of a possible data record information, representing a blacklisted mobile number.
    </summary>
*/
public record BlacklistedMobileNumber : IBlacklistMobileNumber
{
    /** 
        <inheritdoc />
    */
    public long? Id { get; set; }

    /** 
        <inheritdoc />
    */
    public string MobileNumber { get; set; }

    /**
        <summary>
            The constructor of the blacklisted mobile number's possible data record.
        </summary>
        <param name="MobileNumber">string. The mobile number to be blacklisted.</param>
        <param name="Id">[optional] long nullable. The Id of the supposed record.</param>
    */
    public BlacklistedMobileNumber(string MobileNumber, long? Id = null)
    {
        this.Id = Id;
        this.MobileNumber = MobileNumber;
    }
}
