using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace BusConductor.UI.IntegrationTests.Helpers
{
    public static class ScriptRunner
    {
        public static void RunScript()
        {
            const string connectionString = @"Data Source=localhost\sql2008r2;Initial Catalog=BusConductor;Integrated Security=true;";
            var filePath = Environment.CurrentDirectory + "\\..\\..\\..\\SqlScripts\\";
            var file = new FileInfo(filePath + "00001_CreateDatabases.sql");
            var script = file.OpenText().ReadToEnd();
            var connection = new SqlConnection(connectionString);
            var server = new Server(new ServerConnection(connection));
            server.ConnectionContext.ExecuteNonQuery(script);
        }
    }
}
