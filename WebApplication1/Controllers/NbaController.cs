using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using WebApplication1.Controllers.Parameters;
using WebApplication1.Controllers.ViewModels;
using WebApplication1.Repository;
using WebApplication1.Repository.Helpers;

namespace WebApplication1.Controllers
{
    [RoutePrefix("Nba")]
    public class NbaController : ApiController
    {
        private readonly IDatabaseHelper _databaseHelper;

        public NbaController(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        [Route("All")]
        [HttpGet]
        public IEnumerable<NbaViewModel> GetAll()
        {

            using (var conn = _databaseHelper.GetConnection())
            {
                var sqlCommand = "SELECT * FROM [dbo].[NBA]";
                var result = conn.Query<NbaViewModel>(sqlCommand);
                return result;
            }
        }

        [Route("{Name}")]
        [HttpGet]
        public IEnumerable<NbaViewModel> GetByName([FromUri] NbaPrimaryParameter param)
        {
            IEnumerable<NbaViewModel> result = null;

            using (var conn = _databaseHelper.GetConnection())
            {
                var sqlCommand = $"SELECT * FROM [dbo].[NBA] WHERE Name = @Name";
                result = conn.Query<NbaViewModel>(sqlCommand, param);
                return result;
            }
        }

        [Route()]
        [HttpPost]
        public string Post([FromBody] NbaParameter param)
        {
            var result = -999;
            try
            {
                using (var conn = _databaseHelper.GetConnection())
                {
                    var sqlCommand = @"
                     INSERT INTO [dbo].[NBA]
                                ([Name]
                                ,[Team]
                                ,[Position])
                          VALUES
                                (@Name
                                ,@Team
                                ,@Position)";
                    result = conn.Execute(sqlCommand, param);
                }
            }
            catch (Exception ex)
            {
                return $"SqlException: {ex}";
            }


            return result == 1 ? "add success" : "add failed";
        }

        [Route()]
        [HttpPut]
        public string Put([FromBody] NbaParameter param)
        {
            var result = -999;
            try
            {
                using (var conn = _databaseHelper.GetConnection())
                {
                    var sqlCommand = @"
                     UPDATE [dbo].[NBA]
                        SET [Name] = @Name
                           ,[Team] = @Team
                           ,[Position] = @Position
                     WHERE Name = @Name";
                    result = conn.Execute(sqlCommand, param);
                }
            }
            catch (Exception ex)
            {
                return $"SqlException: {ex}";
            }
            return result == 1 ? "update success" : "update failed";
        }

        [Route("{Name}")]
        [HttpDelete]
        public string Delete([FromUri] NbaPrimaryParameter param)
        {
            var result = -999;

            try
            {
                using (var conn = _databaseHelper.GetConnection())
                {
                    var sqlCommand = $"DELETE FROM [dbo].[NBA] WHERE Name = @Name";
                    result = conn.Execute(sqlCommand, param);
                }
            }
            catch (Exception ex)
            {
                return $"SqlException: {ex}";
            }


            return result == 1 ? "delete success" : $"delete failed";
        }

        [Route("All")]
        [HttpDelete]
        public string DeleteAll()
        {
            var result = -999;

            try
            {
                using (var conn = _databaseHelper.GetConnection())
                {
                    var sqlCommand = $"TRUNCATE TABLE [dbo].[NBA]";
                    result = conn.Execute(sqlCommand);
                }
            }
            catch(Exception ex)
            {
                return $"SqlException: {ex}";
            }

            return result == -1 ? "truncate success" : "truncate failed";
        }
    }
}
