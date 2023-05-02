using RestAPIServer.Interface;
using RestAPIServer.Models;

namespace RestAPIServer.Engines;

/// <summary>
/// 
/// </summary>
public class ProductEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="InterestFreeMonths"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<IProduct> Create(string Name, string Description, string InterestFreeMonths, string MinimumDurationMonths, long? Id = null)
    {
        IProduct Result;

        Result = new Product(Name, Description, InterestFreeMonths, MinimumDurationMonths, Id);

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Product"></param>
    /// <returns></returns>

    public Task<bool> IsValid(IProduct Product) 
    {
        bool Result;

        Result = IsValidName(Product.Name).Result ;

        return Task.FromResult(Result); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="InterestFreeMonths"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<bool> IsValid(string Name, string Description, string InterestFreeMonths, long? Id = null) { return Task.FromResult(true); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Name"></param>
    /// <returns></returns>
    public Task<bool> IsValidName(string Name) 
    {
        bool Result;

        if (Name.Trim().Length == 0) Result = false;
        else Result = true;

        return Task.FromResult(Result); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="InterestFreeMonths"></param>
    /// <returns></returns>
    public Task<bool> IsInterestFreeMonths(string InterestFreeMonths) { return Task.FromResult(true); }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<IProduct>> GetAll()
    {
        IEnumerable<IProduct> Result;
        Result = new List<IProduct>();

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Product"></param>
    /// <returns></returns>
    public Task<IProduct> Find(IProduct Product) 
    {
        IProduct Result;

        Result = new Product("", "", "", "");

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<IProduct> Find(long Id) 
    {
        IProduct Result;

        Result = new Product("", "", "", "");

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Name"></param>
    /// <returns></returns>
    public Task<IProduct> Find(string Name) 
    {
        IProduct Result;

        Result = new Product("", "", "", "");

        return Task.FromResult(Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Product"></param>
    /// <returns></returns>
    public Task<bool> IsExisting(IProduct Product) { return Task.FromResult(true); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<bool> IsExisting(long Id) { return Task.FromResult(true); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Name"></param>
    /// <returns></returns>
    public Task<bool> IsExisting(string Name) { return Task.FromResult(true); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Product"></param>
    /// <returns></returns>
    public Task<bool> Save(IProduct Product) { return Task.FromResult(true); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="InterestFreeMonths"></param>
    /// <returns></returns>
    public Task<bool> Save(string Name, string Description, string InterestFreeMonths) { return Task.FromResult(true); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="NewProduct"></param>
    /// <param name="CurrentProduct"></param>
    /// <returns></returns>
    public Task<bool> Update(IProduct NewProduct, IProduct CurrentProduct) { return Task.FromResult(true); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="NewName"></param>
    /// <param name="NewDescription"></param>
    /// <param name="NewInterestFreeMonths"></param>
    /// <param name="CurrentName"></param>
    /// <param name="CurrentDescription"></param>
    /// <param name="CurrentInterestFreeMonths"></param>
    /// <returns></returns>
    public Task<bool> Update(string NewName, string NewDescription, string NewInterestFreeMonths, string CurrentName, string CurrentDescription, string CurrentInterestFreeMonths) { return Task.FromResult(true); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="InterestFreeMonths"></param>
    /// <returns></returns>
    public Task<bool> Update(long Id, string Name, string Description, string InterestFreeMonths) { return Task.FromResult(true); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Product"></param>
    /// <returns></returns>
    public Task<bool> Update(IProduct Product) { return Task.FromResult(true); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Product"></param>
    /// <returns></returns>
    public Task<bool> Delete(IProduct Product) { return Task.FromResult(true); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<bool> Delete(long Id) { return Task.FromResult(true); }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="Name"></param>
    /// <returns></returns>
    public Task<bool> Delete(string Name) { return Task.FromResult(true); }
}
