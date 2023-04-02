namespace RestAPIServer.Engines
{
    public class BlacklistedDomainEngine
    {
        private IConfiguration Configuration { get; set; }

        public BlacklistedDomainEngine(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
    }
}
