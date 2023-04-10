using RestAPIServer.Interface;
using System.Text.Json.Serialization;

namespace RestAPIServer.Models;

/** 
    <summary>
        A set of a possible data record information, representing a blacklisted mobile number.
    </summary>
*/
public record BlacklistedMobileNumber : IBlacklistMobilenumber
{
    /** 
        <inheritdoc />
    */
    public Int64? id { get; private set; }

    /** 
        <inheritdoc />
    */
    public string mobilenumber { get; private set; }

    /**
        <summary>
            The constructor of the blacklisted mobile number's possible data record.
        </summary>
        <param name="MobileNumber">string. The mobile number to be blacklisted.</param>
        <param name="Id">[optional] long nullable. The Id of the supposed record.</param>
    */
    public BlacklistedMobileNumber(string mobilenumber, long? id = null)
    {
        this.id = id;
        this.mobilenumber = mobilenumber;
    }
}
