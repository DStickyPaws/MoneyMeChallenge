using System.Text.Json.Serialization;

namespace RestAPIServer.Interface;

/** 
    <summary>
        This interface is based on the document provided my the moneyme challenge.
    </summary>
*/
public interface IInformation
{
    /** 
        <summary>
            The amount being loaned.
        </summary>
        <remarks>
            Decimal. The amount loaned by the client to the company.
        </remarks>
    */
    [JsonPropertyName("AmountRequired")]
    decimal AmountRequired { get; }
    /** 
        <summary>
            The term of the loan.
        </summary>
        <remarks>
            int / Int32. The duration of the loan. Although in this case it would be ambigous since its not know if this loan is in days / montth / years.
        </remarks>
    */
    [JsonPropertyName("Term")]
    int Term { get; }
    /** 
        <summary>
            The title of the person.
        </summary>
        <remarks>
            string. This property is used in formality with regards to addressing the client. 
        </remarks>
    */
    [JsonPropertyName("Title")]
    string Title { get; }
    /** 
        <summary>
            The first name of the client.
        </summary>
        <remarks>
            string. This property contains the first name of the client. 
        </remarks>
    */
    [JsonPropertyName("FirstName")]
    string FirstName { get; }
    /** 
        <summary>
            The last name of the client.
        </summary>
        <remarks>
            string. This property contains the last name of the client.
        </remarks>
    */
    [JsonPropertyName("LastName")]
    string LastName { get; }
    /** 
        <summary>
            The client's date of birth.
        </summary>
        <remarks>
            string. This property is used to containt the date of birth of the client and is also used in deriving the age of the client.
        </remarks>
    */
    [JsonPropertyName("DateOfBirth")]
    string DateOfBirth { get; }
    /** 
        <summary>
            The client's mobile number. 
        </summary>
        <remarks>
            string. This property contains the client mobile number contact info.
        </remarks>
    */
    [JsonPropertyName("Mobile")]
    string Mobile { get; }
    /** 
        <summary>
            The client's email address. 
        </summary>
        <remarks>
            string. This property contains the client email address.
        </remarks>
    */
    [JsonPropertyName("Email")]
    string Email { get; }
    
}