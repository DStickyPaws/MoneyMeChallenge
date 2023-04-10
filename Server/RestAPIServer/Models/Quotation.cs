using RestAPIServer.Interface;

namespace RestAPIServer.Models;

/**
    <summary>
        A set of a possible data record information, representing a qoutation.
    </summary>    
*/
public class Quotation : IQuotation
{
    /**
        <inheritdoc />
    */
    public long? Id { get; set; }
    /**
        <inheritdoc />
    */
    public long PersonId { get; set; }
    /**
        <inheritdoc />
    */
    public decimal FinancedAmount { get; set; }
    /**
        <inheritdoc />
    */
    public decimal Repayments { get; set; }
    /**
        <inheritdoc />
    */
    public decimal TotalRepayment { get; set; }
    /**
        <inheritdoc />
    */
    public decimal InterestAmount { get; set; }

    /**
        <summary>
            The constructor of the quotation's possible data record.
        </summary>
        <param name="PersonId">long. The Id representating the data record of a quotation.</param>
        <param name="FinancedAmount">decimal. The amount loaned or financed.</param>
        <param name="Repayments">decimal. The repayment amount of the loaned or financed amount.</param>
        <param name="TotalRepayment"> decimal. The total repayment amount.</param>
        <param name="InterestAmount">decimal. The amount added to the total amount to pay as interest.</param>
        <param name="Id">[optional] long nullable. The Id of the supposed record.</param>
    */
    public Quotation(long PersonId, decimal FinancedAmount, decimal Repayments, decimal TotalRepayment, decimal InterestAmount, long? Id = null)
    {
        this.Id = Id;
        this.PersonId = PersonId;
        this.InterestAmount = InterestAmount;
        this.Repayments = Repayments;
        this.TotalRepayment = TotalRepayment;
        this.InterestAmount = InterestAmount;   
        this.FinancedAmount = FinancedAmount;
    }
}
