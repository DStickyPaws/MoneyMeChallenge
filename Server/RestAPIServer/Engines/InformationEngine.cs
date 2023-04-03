using System.Data.SQLite;
using RestAPIServer.Interface;
using RestAPIServer.Models;
using System.Data;
using Dapper;


namespace RestAPIServer.Engines;

internal class InformationEngine
{
    private IConfiguration configuration { get; set; }
    private string ConnectionString { get; set; }

    public InformationEngine(IConfiguration iConfiguration)
    {
        this.configuration = iConfiguration;
        this.ConnectionString = GetConnectionstring().Result;
    }

    private Task<string> GetConnectionstring()
    {
        string Result;
        string? ConnectionString;
        
        ConnectionString = configuration.GetConnectionString("sqlLite");

        Result = ConnectionString ?? string.Empty;

        return Task.FromResult(Result);
    }

    internal Task<IEnumerable<IInformation>> GetInformations()
    {
        IEnumerable<IInformation> Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            var Informations = dbConnection.Query<Information>("select * from Information", new DynamicParameters());
            Result = Informations;
        }

        return Task.FromResult(Result);

    }

    internal Task<bool> SaveInformation(IInformation paramInformation)
    {
        bool Result;

        using (IDbConnection dbConnection = new SQLiteConnection(this.ConnectionString))
        {
            try
            {
                dbConnection.Execute("INSERT into Information () VALUES ()");
                Result = true;
            }
            catch
            {
                Result = false;
                throw;
            }
        }

        return Task.FromResult(Result);
        
    }

    internal Task<IInformation> Find()
    {
        IInformation Result;

        Result = new Information("", "", "", "", "", "", "", "");

        return Task.FromResult(Result);
    }

    public static Task<decimal> PMT(decimal AmountLoaned, int term, float InterestRate, bool type)
    {
        decimal Result, Dividend, Divisor;
        Dividend = 0;
        Divisor = 0;

        /**
            Formula : 
                PMT = P(APR/n) / (1-(1+(APR/N))^(N*T))
        */

        Result = Dividend / Divisor;

        return Task.FromResult(Result);
    }
}
