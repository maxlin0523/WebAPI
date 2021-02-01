using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data.Entity;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    [RoutePrefix("player")]
    public class PlayerController : ApiController
    {
        public const string db_NBA = "Server=tcp:maxlin.database.windows.net,1433;Initial Catalog=maxlin0523;Persist Security Info=False;User ID=happytieu;Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static SqlConnection DapperNBA = new SqlConnection(db_NBA);
        private static maxlin0523Entities EntitiesNBA = new maxlin0523Entities();

        // /player/get/true
        [Route("get/{isDP}")]
        [HttpGet]
        public IEnumerable<NBA> Get(bool isDP)
        {
            IEnumerable<NBA> result = null;
            var DPstr = "SELECT * FROM [dbo].[NBA]";
            if (isDP)
            {
                result = DapperNBA.Query<NBA>(DPstr);
            }
            else
            {
                result = EntitiesNBA.NBA.Select(c => c);
            }
            return result;
        }

        // /player/get/A/true
        [Route("get/{name}/{isDP}")]
        [HttpGet]
        public IEnumerable<NBA> GetByName(string name, bool isDP)
        {
            IEnumerable<NBA> result = null;

            if (isDP)
            {
                var DPstr = $"SELECT * FROM [dbo].[NBA] WHERE Name = '{name}'";
                result = DapperNBA.Query<NBA>(DPstr);
            }
            else
            {
                result = EntitiesNBA.NBA.Where(c => c.Name == name);
            }
            return result;
        }
        // https://localhost:44355/player/post/{"name": "A", "position": "SG", "team": "A_TEAM"}/true
        // {"name": "A", "position": "SG", "team": "A_TEAM"}
        [Route("post/{json}/{isDP}")]
        [HttpGet]
        public string Post(string json, bool isDP)
        {
            int result = 1;
            var jsonObject = JsonConvert.DeserializeObject<NBA>(json);
            var exception = string.Empty;

            try
            {
                if (isDP)
                {
                    var DPstr = @"
                     INSERT INTO [dbo].[NBA]
                                ([Name]
                                ,[Team]
                                ,[Position])
                          VALUES
                                (@Name
                                ,@Team
                                ,@Position)";
                    result = DapperNBA.Execute(DPstr, jsonObject);
                }
                else
                {
                    EntitiesNBA.NBA.Add(jsonObject);
                    EntitiesNBA.SaveChanges();
                }
            }
            catch (Exception b)
            {
                exception = b.ToString();
                result = 0;
            }
            return result == 1 ? "SUCCESS" : $"FAIL\n{exception}";
        }

        [Route("put/{json}/{isDP}")]
        [HttpPut]
        public string Put(string json, bool isDP)
        {
            int result = 0;
            var exception = string.Empty;
            try
            {
                var jsonObject = JsonConvert.DeserializeObject<NBA>(json);
                if (isDP)
                {
                    var name = jsonObject.Name;
                    var team = jsonObject.Team;
                    var position = jsonObject.Position;
                    var str =
                    $@"
                     UPDATE [dbo].[NBA]
                        SET [Name] = '{name}'
                           ,[Team] = '{team}'
                           ,[Position] = '{position}'
                     WHERE Name = '{name}'";
                    result = DapperNBA.Execute(str);
                }
                else
                {
                    var data = EntitiesNBA.NBA.Find(jsonObject.Name);
                    data.Name = jsonObject.Name;
                    data.Position = jsonObject.Position;
                    data.Team = jsonObject.Team;
                    EntitiesNBA.SaveChanges();
                }
            }
            catch (Exception b)
            {
                exception = b.ToString();
                result = 0;
            }
            return result == 1 ? "SUCCESS" : $"FAIL\n{exception}";
        }

        // DELETE api/values/5
        [Route("delete/{id}")]
        [HttpGet]
        public void Delete(int id)
        {
            var s = "y";
        }
    }
}
