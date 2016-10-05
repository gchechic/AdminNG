using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace AdminNG.DAL
{
    public class AdminNGConfiguration : DbConfiguration
    {
        public AdminNGConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}