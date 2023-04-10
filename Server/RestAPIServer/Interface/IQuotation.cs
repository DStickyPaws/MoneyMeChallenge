namespace RestAPIServer.Interface;
/**
    <summary>
        A data record representation of a quotation.    
    </summary>
*/
public interface IQuotation
{
    /**
        <summary>
            The Id representating the data record of a quotation.
        </summary>
    */
    long? Id { get; set; }
    /**
        <summary>
            The Id of the person in which the quotation is provided for.
        </summary>
    */
    long PersonId { get; set; }
    /**
        <summary>
            The amount loaned or financed.
        </summary>
    */
    decimal FinancedAmount { get; set; }
    /**
        <summary>
            The repayment amount of the loaned or financed amount.
        </summary>
    */
    decimal Repayments { get; set; }
    /**
        <summary>
            The total repayment amount.
        </summary>
    */
    decimal TotalRepayment { get; set; }
    /**
        <summary>
            The amount added to the total amount to pay as interest.
        </summary>
    */
    decimal InterestAmount { get; set; }
}
