using RestAPIServer.Interface;

namespace RestAPIServer.Models;

internal record Information : IInformation
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
    public DateOnly DateOfBirth { get; private set; }
    /**
        <inheritdoc />
    */
    public string Mobile { get; private set; }
    /**
        <inheritdoc />
    */
    public string Email { get; private set; }

    public Information(decimal AmountRequired, int Term, string Title, string FirstName, string LastName, DateOnly DateOfBirth, string Mobile, string Email)
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

    public static Information Create(decimal AmountRequired, int Term, string Title, string FirstName, string LastName, DateOnly DateOfBirth, string Mobile, string Email)
    {
        return new Information(AmountRequired, Term, Title, FirstName, LastName, DateOfBirth, Mobile, Email);
    }
}
