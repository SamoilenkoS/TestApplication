using Microsoft.Extensions.Configuration;

namespace WebApplication
{
    public class ConnectionForDb : IConnectionForDb
    {
        public ConnectionForDb(IConfiguration configuration)
        {
            DefaultConnection = configuration["ConnectionStrings:DefaultConnection"];
        }

        public string DefaultConnection { get; set; }
    }
}
