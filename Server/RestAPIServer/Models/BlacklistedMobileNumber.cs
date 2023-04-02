using RestAPIServer.Interface;

namespace RestAPIServer.Models;

/** 
    <summary>
        The business model for the IBlacklistMobilenumber.
    </summary>
*/
public record BlacklistedMobileNumber : IBlacklistMobilenumber
{
    /** 
        <inheritdoc />
    */
    public int? id { get; private set; }

    /** 
        <inheritdoc />
    */
    public string mobilenumber { get; private set; }

    /** 
        <summary>
            
        </summary>
        <param name="id">
        </param>
        <param name="mobilenumber">
        </param>
    */
    public BlacklistedMobileNumber(string mobilenumber)
    {
        this.mobilenumber = mobilenumber;
    }

    public BlacklistedMobileNumber(int id, string mobilenumber)
    {
        this.id = id;
        this.mobilenumber = mobilenumber;
    }

    /** 
        <summary>
        
        </summary>
        <param name="id">
        </param>
        <param name="mobilenumber">
    </param>
    */
    public static Task<IBlacklistMobilenumber> Create(string mobilenumber) 
    {
        return Task.FromResult((IBlacklistMobilenumber)new BlacklistedMobileNumber(mobilenumber));
    }

    public static Task<IBlacklistMobilenumber> Create(int id, string mobilenumber)
    {
        return Task.FromResult((IBlacklistMobilenumber)new BlacklistedMobileNumber(id, mobilenumber));
    }
}
