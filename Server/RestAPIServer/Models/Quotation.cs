using RestAPIServer.Interface;

namespace RestAPIServer.Models;

public class Quotation : IQuotation
{
    /// <summary>
    /// 
    /// </summary>
    public long? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long PersonId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal FinancedAmount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal Repayments { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal TotalRepayment { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal InterestAmount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PersonId"></param>
    /// <param name="FinancedAmount"></param>
    /// <param name="Repayments"></param>
    /// <param name="TotalRepayment"></param>
    /// <param name="InterestAmount"></param>
    /// <param name="Id"></param>
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
