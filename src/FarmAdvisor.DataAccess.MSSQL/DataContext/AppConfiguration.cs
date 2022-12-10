using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Identity.Client;

namespace FarmAdvisor.DataAccess.MSSQL.DataContext
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            var ConfigBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "local.settings.json");
            ConfigBuilder.AddJsonFile(path, false);
            var root = ConfigBuilder.Build();
            var LocalSettings = root.GetSection("ConnectionStrings:DefaultConnection");
            SqlConnectionString = LocalSettings.Value;

        }

        public String SqlConnectionString { get; set; }

    }
}
