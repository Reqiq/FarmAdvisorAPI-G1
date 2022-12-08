using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace FarmAdvisor.DataAccess.MSSQL.DataContext
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            SqlConnectionString = "Data Source=\\SQLEXPRESS;Initial Catalog=farm_api_try1;Integrated Security=True;TrustServerCertificate=True;";
        }

        public String SqlConnectionString { get; set; }

    }
}
