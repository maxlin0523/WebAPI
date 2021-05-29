using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Repository.Helpers
{
    public interface IDatabaseHelper
    {
       IDbConnection GetConnection();
    }
}
