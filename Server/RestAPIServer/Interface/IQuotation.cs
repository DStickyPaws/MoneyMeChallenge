namespace RestAPIServer.Interface;

public interface IQuotation
{
    /// <summary>
    /// 
    /// </summary>
    long? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    long PersonId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    decimal FinancedAmount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    decimal Repayments { get; set; }
    /// <summary>
    /// 
    /// </summary>
    decimal TotalRepayment { get; set; }
    /// <summary>
    /// 
    /// </summary>
    decimal InterestAmount { get; set; }
}
