namespace RestAPIServer.Interface;

/** 

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
    decimal AmountRequired { get; }
    /** 
        <summary>
            The term of the loan.
        </summary>
        <remarks>
            Int / Int32. The duration of the loan. Although in this case it would be ambigous since its not know if this loan is in days / montth / years.
        </remarks>
    */
    int Term { get; }
    /** 
        <summary>
        </summary>
        <remarks>
        </remarks>
    */
    string Title { get; }
    /** 
        <summary>
        </summary>
        <remarks>
        </remarks>
    */
    string FirstName { get; }
    /** 
        <summary>
        </summary>
        <remarks>
        </remarks>
    */
    string LastName { get; }
    /** 
        <summary>
        </summary>
        <remarks>
        </remarks>
    */
    DateOnly DateOfBirth { get; }
    /** 
        <summary>
        </summary>
        <remarks>
        </remarks>
    */
    string Mobile { get; }
    /** 
        <summary>
        </summary>
        <remarks>
        </remarks>
    */
    string Email { get; }
    
}