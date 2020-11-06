using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.Infrastructure.Logs
{
    public class SerilogProvider
    {
        public static void StartWithMysql(string connectionString,string tableName="app_logs")
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .WriteTo.MySQL(connectionString,tableName)
                        .CreateLogger();
        }
    }
}
