using RestAPIServer.Interface;
using System.Text.Json.Serialization;

namespace RestAPIServer.Models;

public record Product : IProduct
{
    /** 
        <inheritdoc />
    */
    public long? Id { get; set; }
    /** 
       <inheritdoc />
    */
    public string Name { get; set; }
    /**
        <inheritdoc />
    */
    public string Description { get; set; }
    /** 
        <inheritdoc />
    */
    public string InterestFreeMonths { get; set; }
    /**
     <inheritdoc />
    */
    public string MinimumDurationMonths { get; set; }

    /**
        <summary>
            The constructor of the product's possible data record.
        </summary>
        <param name="Name">string. The name of the product.</param>
        <param name="Description">string. A short description of the product.</param>
        <param name="InterestFreeMonths">string. The number of Interest Free Months of the Product.</param>
        <param name="MinimumDurationMonths">string. The term of the product's minimum duration (in months)</param>
        <param name="Id">[optional] long nullable. The Id of the supposed record.</param>
    */
    [JsonConstructor]
    public Product(string Name, string Description, string InterestFreeMonths, string MinimumDurationMonths, long? Id = null)
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.InterestFreeMonths = InterestFreeMonths;        
        this.MinimumDurationMonths = MinimumDurationMonths;
    }
}