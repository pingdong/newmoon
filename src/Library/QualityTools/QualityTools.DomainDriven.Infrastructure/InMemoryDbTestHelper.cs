using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace PingDong.QualityTools.Infrastrucutre.SqlServer
{
    public class InMemoryDbTestHelper
    {
        // Note: SQLServer Express has to be installed ahead.

        private const string ServerName = "(localdb)\\mssqllocaldb";

        public static Dictionary<string, string> BuildDatabaseConnectionSetting(string dbName)
        {
            return new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultDbConnection", BuildConnectionString(dbName) }
                };
        }

        // Physical db location: C:\Users\[Username]
        public static string BuildConnectionString(string dbName)
        {
            return $"Server={ServerName};Database={dbName};Trusted_Connection=True;ConnectRetryCount=0";
        } 
        
        public static void CleanUp(string dbName)
        {
            var srv = new Server(ServerName);

            // Close all active connections
            srv.KillAllProcesses(dbName);
            // Remove db file
            var db = srv.Databases[dbName];
            db?.Drop();
        }
    }
}
