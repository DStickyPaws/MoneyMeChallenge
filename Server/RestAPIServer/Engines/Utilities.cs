namespace RestAPIServer.Engines;

/// <summary>
/// 
/// </summary>
public class Utilities
{
    /**
        <summary>
            Obtains the connection string stored within the application's configuration file.
        </summary>
        <param name="AppConfgiuration">IConfiguration. The Application's Configuration wrapped as IConfiguration interface.</param>
        <returns>
            string. The connection string stored within the application's configuration file.
        </returns>
    */
    public static Task<string> GetConnectionString(IConfiguration AppConfgiuration)
    {
        string? ConnectionString;
        string Result;

        ConnectionString = AppConfgiuration.GetConnectionString("sqlLite");

        Result = ConnectionString ?? string.Empty;

        return Task.FromResult(Result);
    }
}
