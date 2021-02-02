using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("player")]
    public class PlayerController : ApiController
    {
        private readonly SqlConnection _dapper;
        private readonly maxlin0523Entities _entities;
        public PlayerController()
        {
            _dapper = DataCenter.DapperNBA;
            _entities = DataCenter.EntitiesNBA;
        }

        // /player/get/true
        [Route("get/{isDP}")]
        [HttpGet]
        public IEnumerable<NBA> Get(bool isDP)
        {
            IEnumerable<NBA> result = null;
           
            var DPstr = "SELECT * FROM [dbo].[NBA]";

            if (isDP)
            {
                result = _dapper.Query<NBA>(DPstr);
            }
            else
            {
                result = _entities.NBA.ToList();
            }
            return result;
        }

        [Route("get/{name}/{isDP}")]
        [HttpGet]
        public NBA GetByName(string name, bool isDP)
        {
            NBA result = null;

            if (isDP)
            {
                var DPstr = $"SELECT * FROM [dbo].[NBA] WHERE Name = '{name}'";
                result = _dapper.QuerySingle<NBA>(DPstr);
            }
            else
            {
                result = _entities.NBA.Single(c => c.Name == name);
            }
            return result;
        }
        // https://localhost:44355/player/post/{"name": "A", "position": "SG", "team": "A_TEAM"}/true
        // {"name": "A", "position": "SG", "team": "A_TEAM"}
        [Route("post/{json}/{isDP}")]
        [HttpGet]
        public string Post(string json, bool isDP)
        {
            var result = 1;
            var exception = string.Empty;

            try
            {
                var jsonObject = JsonConvert.DeserializeObject<NBA>(json);
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
                    result = _dapper.Execute(DPstr, jsonObject);
                }
                else
                {
                    _entities.NBA.Add(jsonObject);
                    _entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                result = 0;
            }

            return result == 1 ? "SUCCESS" : $"FAIL\n{exception}";
        }

        [Route("put/{json}/{isDP}")]
        [HttpGet]
        public string Put(string json, bool isDP)
        {
            var result = 1;
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
                    result = _dapper.Execute(str);
                }
                else
                {
                    var data = _entities.NBA.Find(jsonObject.Name);
                    data.Name = jsonObject.Name;
                    data.Position = jsonObject.Position;
                    data.Team = jsonObject.Team;
                    _entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                result = 0;
            }

            return result == 1 ? "SUCCESS" : $"FAIL\n{exception}";
        }

        [Route("delete/{name}/{isDP}")]
        [HttpGet]
        public string Delete(string name,bool isDP)
        {
            var result = 1;
            var exception = string.Empty;

            try
            {
                if (isDP)
                {
                    var DPstr = $"DELETE FROM [dbo].[NBA] WHERE Name = '{name}'";
                    result = _dapper.Execute(DPstr);
                }
                else
                {
                    var data = _entities.NBA.Single(c => c.Name == name);
                    _entities.NBA.Remove(data);
                    _entities.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                exception = ex.ToString();
                result = 0;                  
            }

            return result == 1 ? "SUCCESS" : $"FAIL\n{exception}";
        }
    }
}
