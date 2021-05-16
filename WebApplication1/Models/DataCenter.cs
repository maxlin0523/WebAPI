using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class DataCenter
    {
        private static string BasketballLeagueConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

        public SqlConnection BasketballLeagueDatabase = new SqlConnection(BasketballLeagueConnectionString);

        private DataCenter()
        {

        }

        private static DataCenter _instance;
         
        public static DataCenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataCenter();
                }

                return _instance;
            }
        }
    }
}