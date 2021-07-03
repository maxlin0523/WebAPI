using EF.Diagnostics.Profiling;
using EF.Diagnostics.Profiling.Data;
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
        
        public IDbConnection GetConnection()
        {
            var dbConnection = new SqlConnection(_basketballLeagueConnectionString);

            var dbProfiler = new DbProfiler(ProfilingSession.Current.Profiler);

            return new ProfiledDbConnection(dbConnection, dbProfiler);
        }
    }
}