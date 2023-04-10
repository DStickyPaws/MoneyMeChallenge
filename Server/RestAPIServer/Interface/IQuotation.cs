using System.Text.Json.Serialization;

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
    [JsonPropertyName("Id")]
    long? Id { get; set; }
    /**
        <summary>
            The Id of the person in which the quotation is provided for.
        </summary>
    */
    [JsonPropertyName("PersonId")]
    long PersonId { get; set; }
    /**
        <summary>
            The amount loaned or financed.
        </summary>
    */
    [JsonPropertyName("FinancedAmount")]
    decimal FinancedAmount { get; set; }
    /**
        <summary>
            The repayment amount of the loaned or financed amount.
        </summary>
    */
    [JsonPropertyName("Repayments")]
    decimal Repayments { get; set; }
    /**
        <summary>
            The total repayment amount.
        </summary>
    */
    [JsonPropertyName("TotalRepayment")]
    decimal TotalRepayment { get; set; }
    /**
        <summary>
            The amount added to the total amount to pay as interest.
        </summary>
    */
    [JsonPropertyName("InterestAmount")]
    decimal InterestAmount { get; set; }
}
