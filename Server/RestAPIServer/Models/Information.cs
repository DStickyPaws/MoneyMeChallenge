using RestAPIServer.Interface;

namespace RestAPIServer.Models;

/**
    <summary>
        A set of a possible data record information, representing a request for quotation information.
    </summary>
*/
public record Information : IInformation
{
    public long? Id { get; set; }
    /**
        <inheritdoc />
    */
    public string AmountRequired { get; set; }
    /**
        <inheritdoc />
    */
    public string Term { get; set; }
    /**
        <inheritdoc />
    */
    public string Title { get; set; }
    /**
        <inheritdoc />
    */
    public string FirstName { get; set; }
    /**
        <inheritdoc />
    */
    public string LastName { get; set; }
    /**
        <inheritdoc />
    */
    public string DateOfBirth { get; set; }
    /**
        <inheritdoc />
    */
    public string Mobile { get; set; }
    /**
        <inheritdoc />
    */
    public string Email { get; set; }

    /** 
        <summary>
            The constructor of the record. 
        </summary>
        <remarks>
            Made it this way to solve the possibility of null reference for the properties.
        </remarks>
    */
    public Information(string AmountRequired, string Term, string Title, string FirstName, string LastName, string DateOfBirth, string Mobile, string Email)
    {
        this.AmountRequired = AmountRequired;
        this.Term = Term;
        this.Title = Title;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.DateOfBirth = DateOfBirth;
        this.Mobile = Mobile;
        this.Email = Email;
    }
}
