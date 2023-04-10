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