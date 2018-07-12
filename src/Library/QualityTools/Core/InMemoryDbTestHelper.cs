using Microsoft.SqlServer.Management.Smo;

namespace PingDong.QualityTools.Core
{
    public class InMemoryDbTestHelper
    {
        // Note: SQLServer Express has to be installed ahead.

        private const string ServerName = "(localdb)\\mssqllocaldb";
        private const string DbName = "Events.InMemory";

        // Physical db location: C:\Users\[Username]
        public static string DefaultConnectionString => $"Server={ServerName};Database={DbName};Trusted_Connection=True;ConnectRetryCount=0";
        
        public static void CleanUp()
        {
            var srv = new Server(ServerName);

            // Close all active connections
            srv.KillAllProcesses(DbName);
            // Remove db file
            var db = srv.Databases[DbName];
            db.Drop();
        }
    }
}
