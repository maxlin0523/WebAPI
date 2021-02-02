using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public static class DataCenter
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

        public static SqlConnection DapperNBA = new SqlConnection(_dbStr);
        public static maxlin0523Entities EntitiesNBA = new maxlin0523Entities();

        static DataCenter()
        {
        }
    }
}