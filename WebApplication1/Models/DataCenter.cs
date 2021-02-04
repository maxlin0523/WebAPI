using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class DataCenter
    {
        private static string _dbStr
        {
            get
            {
                var str = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["DBStr"]);
                str.Password = Tools.Instance.aesDecryptBase64(str.Password, "gss");
                return str.ToString();
            }
        }

        public SqlConnection DapperNBA = new SqlConnection(_dbStr);
        public maxlin0523Entities EntitiesNBA = new maxlin0523Entities();

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