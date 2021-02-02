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

        public static SqlConnection DapperNBA { get; set; }
        public static maxlin0523Entities EntitiesNBA;

        static DataCenter()
        {
            DapperNBA = new SqlConnection(_dbStr);
            EntitiesNBA = new maxlin0523Entities();
        }
    }
}