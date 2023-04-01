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

    public Information()
    {
        
    }
}
