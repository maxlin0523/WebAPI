using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Repository.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private static string _basketballLeagueConnectionString;

        public DatabaseHelper(string connectionString)
        {
            _basketballLeagueConnectionString = connectionString;
        }
        
        public SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_basketballLeagueConnectionString);
            return conn;                
        }
    }
}