using RestAPIServer.Interface;

namespace RestAPIServer.Models;

public record Information : IInformation
{
    /**
        <inheritdoc />
    */
    public decimal AmountRequired { get; private set; }
    /**
        <inheritdoc />
    */
    public int Term { get; private set; }
    /**
        <inheritdoc />
    */
    public string Title { get; private set; }
    /**
        <inheritdoc />
    */
    public string FirstName { get; private set; }
    /**
        <inheritdoc />
    */
    public string LastName { get; private set; }
    /**
        <inheritdoc />
    */
    public string DateOfBirth { get; private set; }
    /**
        <inheritdoc />
    */
    public string Mobile { get; private set; }
    /**
        <inheritdoc />
    */
    public string Email { get; private set; }

    /** 
        <summary>
            The constructor of the record. 
        </summary>
        <remarks>
            Made it this way to solve the possibility of null reference for the properties.
        </remarks>
    */
    public Information(decimal AmountRequired, int Term, string Title, string FirstName, string LastName, string DateOfBirth, string Mobile, string Email)
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

    /**
        <summary>
            A static creation method for the 
        </summary>        
    */
    public static Information Create(decimal AmountRequired, int Term, string Title, string FirstName, string LastName, string DateOfBirth, string Mobile, string Email)
    {
        return new Information(AmountRequired, Term, Title, FirstName, LastName, DateOfBirth, Mobile, Email);
    }
}
