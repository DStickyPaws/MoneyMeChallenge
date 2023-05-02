using System.Text.Json.Serialization;

namespace RestAPIServer.Interface;

/**
    <summary>
        The product's data representation.
    </summary>
*/
public interface IProduct
{
    /**
        <summary>
            The Id of the record representing the product
        </summary>
    */
    [JsonPropertyName("Id")]
    long? Id { get; set; }
    /**
        <summary>
            The name of the product 
        </summary>
    */
    [JsonPropertyName("Name")]
    string Name { get; set; }
    /**
        <summary>
            A short description of the product
        </summary>
    */
    [JsonPropertyName("Description")]
    string Description { get; set; }
    /**
        <summary>
            The number of months that are interest free.
        </summary>
    */
    [JsonPropertyName("InterestFreeMonths")]
    long InterestFreeMonths { get; set; }

    /**
        <summary>
            The number of months that are the minimum term for the product.
        </summary>
    */
    [JsonPropertyName("MinimumDurationMonth")]
    long MinimumDurationMonths { get; set; }
}
