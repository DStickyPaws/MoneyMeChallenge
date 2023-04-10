using RestAPIServer.Interface;
using RestAPIServer.Models;

namespace RestAPIServer.Engines;

public class QuotationEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="PersonId"></param>
    /// <param name="FinancedAmount"></param>
    /// <param name="Repayments"></param>
    /// <param name="TotalRepayment"></param>
    /// <param name="InterestAmount"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<IQuotation> Create(long PersonId, decimal FinancedAmount, decimal Repayments, decimal TotalRepayment, decimal InterestAmount, long? Id = null)
    {
        IQuotation Result;

        Result = new Quotation(PersonId, FinancedAmount, Repayments, TotalRepayment, InterestAmount, Id);

        return Task.FromResult(Result);
    }
}
