using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace TechnicalTest.Models
{
    public class ConfigConnection
    {
        public static string GetSqlConnStr(string connsectionName)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();//Create ConfigurationBuilder object
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();
            return configuration[connsectionName];
        }

    }
}
