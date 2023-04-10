using System.Text.Json.Serialization;

namespace RestAPIServer.Interface;

/** 
    <summary>
        This interface is based on the document provided my the moneyme challenge.
    </summary>
*/
public interface IInformation
{   /**
        <summary>
            The Id representating the data record of a quotation request information.
        </summary>
    */
    int? id { get; set; }
    /** 
        <summary>
            The amount being loaned.
        </summary>
    */
    [JsonPropertyName("AmountRequired")]
    string AmountRequired { get; set; }
    /** 
        <summary>
            The term of the loan.
        </summary>
    */
    [JsonPropertyName("Term")]
    string Term { get; set; }
    /** 
        <summary>
            The title of the person.
        </summary>
    */
    [JsonPropertyName("Title")]
    string Title { get; set; }
    /** 
        <summary>
            The first name of the client.
        </summary>
    */
    [JsonPropertyName("FirstName")]
    string FirstName { get; set; }
    /** 
        <summary>
            The last name of the client.
        </summary>
    */
    [JsonPropertyName("LastName")]
    string LastName { get; set; }
    /** 
        <summary>
            The client's date of birth.
        </summary>
    */
    [JsonPropertyName("DateOfBirth")]
    string DateOfBirth { get; set; }
    /** 
        <summary>
            The client's mobile number. 
        </summary>
    */
    [JsonPropertyName("Mobile")]
    string Mobile { get; set; }
    /** 
        <summary>
            The client's email address. 
        </summary>
    */
    [JsonPropertyName("Email")]
    string Email { get; set; }    
}